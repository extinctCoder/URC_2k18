using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace arcEye_2k18.basicControls
{
    /// <summary>
    /// Interaction logic for statusBar.xaml
    /// </summary>
    public partial class statusBar : UserControl
    {
        private static statusBar _statusBar;

        private statusBar()
        {
            InitializeComponent();
        }

        public static statusBar obj
        {
            get
            {
                if (statusBar._statusBar == null)
                {
                    statusBar._statusBar = new statusBar();
                }

                return statusBar._statusBar;
            }
        }

        public static void status(string content)
        {
            statusBar.obj.left_content.Text = "urc_2k18 : "+content.ToString();
        }

        public static void statusAi(string content)
        {
            statusBar.obj.right_content.Text = content.ToString() + " : urc_2k18-AI";
        }
        public static void status_ThreadSafe(string content)
        {
            try
            {
                lock (statusBar.obj)
                {
                    statusBar.obj.left_content.Dispatcher.Invoke(new Action(() => statusBar.obj.left_content.Text = "urc_2k18 : " + content.ToString()));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        public static void statusAi_ThreadSafe(string content)
        {
            try
            {
                lock (statusBar.obj)
                {
                    statusBar.obj.right_content.Dispatcher.Invoke(new Action(() => statusBar.obj.right_content.Text = content.ToString() + " : urc_2k18-AI"));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
