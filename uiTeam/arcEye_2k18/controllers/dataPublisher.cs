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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace arcEye_2k18.controllers
{
    public partial class dataPublisher
    {
        private IXDBroadcaster broadcast;
        private XDMessagingClient client;
        private Thread _thread;

        public dataPublisher()
        {
            _thread = new Thread(mqttDataPublisher);
            _thread.IsBackground = true;

            client = new XDMessagingClient();
            broadcast = client.Broadcasters
                .GetBroadcasterForMode(XDTransportMode.HighPerformanceUI);
            _thread.Start();

        }
        private void runDataPublisher()
        {
            /*double _value;
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

                    this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                        new statusBarData(statusBarPoint.Normal, "injected value is : "+_value.ToString()));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            */
        }
    }

    public partial class dataPublisher
    {
        private String brokerIp = "192.168.0.101";

        private string sensorSoil = "sensor/soil";
        private string sensorUv = "sensor/uv";
        private string sensorGas = "sensor/gas";
        private string sensorTemp = "sensor/temp";
        private string sensorAirTemp = "sensor/air/temp";
        private string sensorAirHum = "sensor/air/hum";
        private string sensorGps = "sensor/gps_lat_log";
        private string sensorGyro = "sensor/gyro";



        private MqttClient soilClient;
        private MqttClient uvClient;
        private MqttClient gasClient;
        private MqttClient tempClient;
        private MqttClient airTempClient;
        private MqttClient airHumClient;
        private MqttClient gpsClient;
        private MqttClient gyroClient;

        private string soilClientId;
        private string uvClientId;
        private string gasClientId;
        private string tempClientId;
        private string airTempClientId;
        private string airHumClientId;
        private string gpsClientId;
        private string gyroClientId;

        private void mqttDataPublisher()
        {
            
            

            soilClient = new MqttClient(brokerIp);
            uvClient = new MqttClient(brokerIp);
            gasClient = new MqttClient(brokerIp);
            tempClient = new MqttClient(brokerIp);
            airTempClient = new MqttClient(brokerIp);
            airHumClient = new MqttClient(brokerIp);
            gpsClient = new MqttClient(brokerIp);
            gyroClient = new MqttClient(brokerIp);




            soilClient.Subscribe(new string[] { sensorSoil }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            uvClient.Subscribe(new string[] { sensorUv }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            gasClient.Subscribe(new string[] { sensorGas }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            tempClient.Subscribe(new string[] { sensorTemp }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            airTempClient.Subscribe(new string[] { sensorAirTemp }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            airHumClient.Subscribe(new string[] { sensorAirHum }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            gpsClient.Subscribe(new string[] { sensorGps }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            gyroClient.Subscribe(new string[] { sensorGyro }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });



            soilClient.MqttMsgPublishReceived += (s, e) =>
            {
                try
                {
                    this.broadcast.SendToChannel(ChannelListMod.SoilSensor.ToString(),
                        new coloumnChartData() {coloumnChartValue = Convert.ToDouble(Encoding.UTF8.GetString(e.Message))});
                }
                catch (Exception exception)
                {
                    this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                        new statusBarData(statusBarPoint.Ai, "Error : " + exception.Message));
                }
            };

            uvClient.MqttMsgPublishReceived += (s, e) =>
            {
                try
                {
                    this.broadcast.SendToChannel(ChannelListMod.UvSensor.ToString(),
                        new coloumnChartData() { coloumnChartValue = Convert.ToDouble(Encoding.UTF8.GetString(e.Message)) });
                }
                catch (Exception exception)
                {
                    this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                        new statusBarData(statusBarPoint.Ai, "Error : " + exception.Message));
                }
            };

            gasClient.MqttMsgPublishReceived += (s, e) =>
            {
                try
                {
                    this.broadcast.SendToChannel(ChannelListMod.GasSensore.ToString(),
                        new coloumnChartData() { coloumnChartValue = Convert.ToDouble(Encoding.UTF8.GetString(e.Message)) });
                }
                catch (Exception exception)
                {
                    this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                        new statusBarData(statusBarPoint.Ai, "Error : " + exception.Message));
                }
            };

            tempClient.MqttMsgPublishReceived += (s, e) =>
            {
                try
                {
                    this.broadcast.SendToChannel(ChannelListMod.TempSensor.ToString(),
                        new coloumnChartData() { coloumnChartValue = Convert.ToDouble(Encoding.UTF8.GetString(e.Message)) });
                }
                catch (Exception exception)
                {
                    this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                        new statusBarData(statusBarPoint.Ai, "Error : " + exception.Message));
                }
            };

            airTempClient.MqttMsgPublishReceived += (s, e) =>
            {
                try
                {
                    this.broadcast.SendToChannel(ChannelListMod.AirTempSensor.ToString(),
                        new lineChartData() { lineChartValue = Convert.ToDouble(Encoding.UTF8.GetString(e.Message)) });
                }
                catch (Exception exception)
                {
                    this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                        new statusBarData(statusBarPoint.Ai, "Error : " + exception.Message));
                }
            };

            airHumClient.MqttMsgPublishReceived += (s, e) =>
            {
                try
                {
                    this.broadcast.SendToChannel(ChannelListMod.AirHumSensor.ToString(),
                        new lineChartData() { lineChartValue = Convert.ToDouble(Encoding.UTF8.GetString(e.Message)) });
                }
                catch (Exception exception)
                {
                    this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                        new statusBarData(statusBarPoint.Ai, "Error : " + exception.Message));
                }
            };
            gpsClient.MqttMsgPublishReceived += (s, e) =>
            {
                try
                {
                    string data = Encoding.UTF8.GetString(e.Message);
                    string[] datas = data.Split(',');
                    this.broadcast.SendToChannel(ChannelListMod.mapModule.ToString(),
                        new mapModuleData() { xAxisValue = Convert.ToDouble(datas[0]),yAxisValue=Convert.ToDouble(datas[1]) });
                }
                catch (Exception exception)
                {
                    this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                        new statusBarData(statusBarPoint.Ai, "Error : " + exception.Message));
                }
            };
            gyroClient.MqttMsgPublishReceived += (s, e) =>
            {
                try
                {
                    string data = Encoding.UTF8.GetString(e.Message);
                    string[] datas = data.Split(',');
                    this.broadcast.SendToChannel(ChannelListMod.gyroModule.ToString(),
                        new gyroModuleData() { xAxisData = Convert.ToDouble(datas[0]), yAxisData = Convert.ToDouble(datas[1]), zAxisData = Convert.ToDouble(datas[2]) });
                }
                catch (Exception exception)
                {
                    this.broadcast.SendToChannel(ChannelList.statusBar.ToString(),
                        new statusBarData(statusBarPoint.Ai, "Error : " + exception.Message));
                }
            };


            soilClientId = Guid.NewGuid().ToString();
            uvClientId = Guid.NewGuid().ToString();
            gasClientId = Guid.NewGuid().ToString();
            tempClientId = Guid.NewGuid().ToString();
            airTempClientId = Guid.NewGuid().ToString();
            airHumClientId = Guid.NewGuid().ToString();
            gpsClientId = Guid.NewGuid().ToString();
            gyroClientId = Guid.NewGuid().ToString();


            try
            {
                soilClient.Connect(soilClientId);
                uvClient.Connect(uvClientId);
                gasClient.Connect(gasClientId);
                tempClient.Connect(tempClientId);
                airTempClient.Connect(airTempClientId);
                airHumClient.Connect(airHumClientId);
                gpsClient.Connect(gpsClientId);
                gyroClient.Connect(gyroClientId);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
    }

}
