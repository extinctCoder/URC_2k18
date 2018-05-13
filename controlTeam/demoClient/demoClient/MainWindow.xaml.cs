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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.convertAdd();
            try
            {
                this._tcpClient = new TcpClient();
                this._tcpClient.Connect(this._ip,this._port);
                this._networkStream = this._tcpClient.GetStream();
                this._streamReader=new StreamReader(this._networkStream);
                this._streamWriter=new StreamWriter(this._networkStream);
                this._thread = new Thread(() => dataHandler());
                this._thread.IsBackground = true;
                this._thread.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void dataHandler()
        {

            this._streamWriter.AutoFlush = true;
            while (true)
            {
                printMsg(this._streamReader.ReadLine());
            }
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
    }
}
