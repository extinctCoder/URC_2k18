using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace demoClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String _ip;
        private int _port;
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private Thread _thread;
        private bool _threadFlag = false;

        public MainWindow()
        {
            InitializeComponent();
            this._thread = new Thread(() => dataHandler());
            this._thread.IsBackground = true;
            this.disconnectButton.IsEnabled = this._threadFlag;
            this.connectButton.IsEnabled = !this._threadFlag;
        }

        private void ConnectButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.printMsgNoneThread("Connection is starting");
            this.convertAdd();
            this._threadFlag = true;
            this.disconnectButton.IsEnabled = this._threadFlag;
            this.connectButton.IsEnabled = !this._threadFlag;
            try
            {
                this._tcpClient = new TcpClient();
                this._tcpClient.Connect(this._ip,this._port);
                this._networkStream = this._tcpClient.GetStream();
                this._streamReader=new StreamReader(this._networkStream);
                this._streamWriter=new StreamWriter(this._networkStream);
                if (this._thread.IsAlive)
                {
                    this._thread.Resume();
                }
                else
                {
                    this._thread.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void dataHandler()
        {

            this._streamWriter.AutoFlush = true;
            while (this._threadFlag)
            {
                try
                {
                    printMsg(this._streamReader.ReadLine());
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            this._thread.Suspend();
        }

        private void convertAdd()
        {
            try
            {
                this._ip = Convert.ToString(this.ip.Text);
                this._port = Convert.ToInt32(this.port.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void printMsg(String Msg)
        {
            try
            {
                lock (textBox)
                {
                    textBox.Dispatcher.Invoke(new Action(() =>
                        textBox.Text = textBox.Text + "MSG :_ " + Msg + "\n"), DispatcherPriority.Background);
                    textBox.Dispatcher.Invoke(new Action(() =>
                        textBox.ScrollToEnd()), DispatcherPriority.Background);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void printMsgNoneThread(String Msg)
        {
            textBox.Text = textBox.Text + "MSG :_ " + Msg + "\n";
            textBox.ScrollToEnd();
        }

        private void DisconnectButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.printMsgNoneThread("Connection is disconnected");
            this._threadFlag = false;
            this.disconnectButton.IsEnabled = this._threadFlag;
            this.connectButton.IsEnabled = !this._threadFlag;
            if (this._thread.IsAlive)
            {
                this._thread.Suspend();
            }
            this._tcpClient.Close();
            this._tcpClient = null;
            this._networkStream = null;
            this._streamReader = null;
            this._streamWriter = null;
        }
    }
}
