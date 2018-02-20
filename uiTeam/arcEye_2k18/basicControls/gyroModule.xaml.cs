using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HelixToolkit.Wpf;

namespace arcEye_2k18.basicControls
{
    /// <summary>
    /// Interaction logic for gyroModule.xaml
    /// </summary>
    public partial class gyroModule : UserControl
    {
        private static gyroModule _gyroModule;
        private Thread _gyroModulleThread;
        private gyroModule()
        {
            InitializeComponent();
            this.startGyroModule();
        }

        public static gyroModule obj
        {
            get
            {
                if (gyroModule._gyroModule == null)
                {
                    gyroModule._gyroModule=new gyroModule();
                }

                return gyroModule._gyroModule;
            }
        }
    }

    public partial class gyroModule
    {
        private void startGyroModule()
        {
            this._gyroModulleThread = new Thread(() => gyroModuleThreadedFunc());
            this._gyroModulleThread.IsBackground = true;
            this._gyroModulleThread.Name = "gyroModulleThread";
            this._gyroModulleThread.Start();
        }

        private void gyroModuleThreadedFunc()
        {
            Random rnd = new Random();
            while (true)
            {
                try
                {
                    _gyroModule.Dispatcher.Invoke((Action) (() =>
                    {
                        this.rtn_x.Angle = Convert.ToDouble(rnd.Next(0, 5));
                        this.rtn_y.Angle = Convert.ToDouble(rnd.Next(0, 5));
                        this.rtn_z.Angle = Convert.ToDouble(rnd.Next(0, 5));
                       
                    }));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }
    }
}
