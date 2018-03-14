using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using arcEye_2k18.basicControls;
using arcEye_2k18.chartControls;
using arcEye_2k18.controllers;
using Dragablz;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using XDMessaging;

namespace arcEye_2k18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private dataPublisher _dataPublisher;
        public MainWindow()
        {   
            _dataPublisher = new dataPublisher();
            InitializeComponent();
            this.initializeControls();
            this.placeComponentsHome();
            this.placeComponentsConstant();
            this.home_grid.Visibility = Visibility.Visible;
            this.settings_grid.Visibility = Visibility.Hidden;
            this.full_screen_grid.Visibility = Visibility.Hidden;
        }

        private void Home_btn_OnClick(object sender, RoutedEventArgs e)
        {
            this.placeComponentsHome();
            this.WindowState = WindowState.Normal;
            this.home_grid.Visibility = Visibility.Visible;
            this.settings_grid.Visibility = Visibility.Hidden;
            this.full_screen_grid.Visibility = Visibility.Hidden;
            
        }

        private void Full_screen_btn_OnClick(object sender, RoutedEventArgs e)
        {
            this.placeComponentsFullScreen();
            this.WindowState = WindowState.Maximized;
            this.home_grid.Visibility = Visibility.Hidden;
            this.settings_grid.Visibility = Visibility.Hidden;
            this.full_screen_grid.Visibility = Visibility.Visible;
            
        }

        private void Normal_btn_OnClick(object sender, RoutedEventArgs e)
        {
            this.placeComponentsHome();
            this.WindowState = WindowState.Maximized;
            this.home_grid.Visibility = Visibility.Visible;
            this.settings_grid.Visibility = Visibility.Hidden;
            this.full_screen_grid.Visibility = Visibility.Hidden;
            
        }

        private void Settings_btn_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.home_grid.Visibility = Visibility.Hidden;
            this.settings_grid.Visibility = Visibility.Visible;
            this.full_screen_grid.Visibility = Visibility.Hidden;
            
        }
        private void Info_btn_OnClick(object sender, RoutedEventArgs e)
        {
            this.dialog_host.IsOpen = true;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.A || e.Key == Key.D || e.Key == Key.S)
            {
                movementController.roverMovementController(sender,e);
            }
            else if(e.Key == Key.Up || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Down || e.Key == Key.NumPad8 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6)
            {
               movementController.cameraMovementController(sender, e);
            }
            else if (e.Key == Key.F || e.Key == Key.G || e.Key == Key.H || e.Key == Key.J || e.Key == Key.K || e.Key == Key.L || e.Key == Key.C || e.Key == Key.V || e.Key == Key.B || e.Key == Key.N || e.Key == Key.M || e.Key == Key.OemComma)
            {
                movementController.handMovementController(sender,e);
            }
            else if (e.Key == Key.F1)
            {
                this.Home_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (e.Key == Key.F2)
            {
                this.Full_screen_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (e.Key == Key.F3)
            {
                this.Normal_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (e.Key == Key.F4)
            {
                this.Settings_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (e.Key == Key.F12)
            {
                this.dialog_host.IsOpen = true;
            }
            else if (e.Key == Key.LeftShift)
            {
                
            }
            else if (e.Key == Key.LeftCtrl)
            {

            }
            else
            {
                
            }

            e.Handled = true;
        }

        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.A || e.Key == Key.D || e.Key == Key.S)
            {
                movementController.roverMovementController(null, null);
            }
            else if (e.Key == Key.Up || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Down || e.Key == Key.NumPad8 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6)
            {
                movementController.cameraMovementController(null, null);
            }
            else if (e.Key == Key.F || e.Key == Key.G || e.Key == Key.H || e.Key == Key.J || e.Key == Key.K || e.Key == Key.L || e.Key == Key.C || e.Key == Key.V || e.Key == Key.B || e.Key == Key.N || e.Key == Key.M || e.Key == Key.OemComma)
            {
                movementController.handMovementController(null, null);
            }
            else if (e.Key == Key.F1 || e.Key == Key.F2 || e.Key == Key.F3 || e.Key == Key.F4)
            {
                this.Home_btn_OnClick(new ObjectDataProvider(), new RoutedEventArgs());
            }
            else if (e.Key == Key.F12)
            {
                this.dialog_host.IsOpen = false;
            }
            else if (e.Key == Key.LeftShift)
            {

            }
            else if (e.Key == Key.LeftCtrl)
            {

            }
            else
            {
                
            }

            e.Handled = true;
        }

    }

    public partial class MainWindow
    {
        private angularGauge _angularGauge1, _angularGauge2;
        private solidGauge _solidGauge1, _solidGauge2, _solidGauge3, _solidGauge4;
        private columnChart _columnChart1, _columnChart2, _columnChart3, _columnChart4;
        private lineChart _lineChart;
        private mapModule _mapModule;
        private gyroModule _gyroModule;
        private visionModule _visionModule;
        private movementControl _movementControl;
        private void initializeControls()
        {
            this._angularGauge1 = new angularGauge();
            this._angularGauge2 = new angularGauge();
            this._solidGauge1 = new solidGauge();
            this._solidGauge2 = new solidGauge();
            this._solidGauge3 = new solidGauge();
            this._solidGauge4 = new solidGauge();
            this._columnChart1 = new columnChart();
            this._columnChart2 = new columnChart();
            this._columnChart3 = new columnChart();
            this._columnChart4 = new columnChart();
            this._lineChart = new lineChart();
            this._mapModule = new mapModule();
            this._gyroModule = new gyroModule();
            this._visionModule = new visionModule();
            this._movementControl = movementControl.obj;
        }

        private void placeComponentsConstant()
        {
            this.status_bar_grid.Children.Clear();

            this.status_bar_grid.Children.Add(new statusBar());
        }

        private void placeComponentsHome()
        {
            this.angular_gudge_grid_one.Children.Clear();
            this.angular_gudge_grid_two.Children.Clear();
            this.solid_gudge_grid_one.Children.Clear();
            this.solid_gudge_grid_two.Children.Clear();
            this.solid_gudge_grid_three.Children.Clear();
            this.solid_gudge_grid_four.Children.Clear();
            this.column_chart_grid_one.Children.Clear();
            this.column_chart_grid_two.Children.Clear();
            this.column_chart_grid_three.Children.Clear();
            this.column_chart_grid_four.Children.Clear();
            this.line_chart_grid.Children.Clear();
            this.map_grid.Children.Clear();
            this.gyro_grid.Children.Clear();
            this.home_movement_control_grid.Children.Clear();
            this.home_vision_grid.Children.Clear();
            this.full_screen_movement_control_grid.Children.Clear();
            this.full_screen_vision_grid.Children.Clear();

            this.angular_gudge_grid_one.Children.Add(this._angularGauge1);
            this.angular_gudge_grid_two.Children.Add(this._angularGauge2);
            this.solid_gudge_grid_one.Children.Add(this._solidGauge1);
            this.solid_gudge_grid_two.Children.Add(this._solidGauge2);
            this.solid_gudge_grid_three.Children.Add(this._solidGauge3);
            this.solid_gudge_grid_four.Children.Add(this._solidGauge4);
            this.column_chart_grid_one.Children.Add(this._columnChart1);
            this.column_chart_grid_two.Children.Add(this._columnChart2);
            this.column_chart_grid_three.Children.Add(this._columnChart3);
            this.column_chart_grid_four.Children.Add(this._columnChart4);
            this.line_chart_grid.Children.Add(this._lineChart);
            this.map_grid.Children.Add(this._mapModule);
            this.gyro_grid.Children.Add(this._gyroModule);

            this.home_vision_grid.Children.Add(this._visionModule);

            this.home_movement_control_grid.Children.Add(this._movementControl);

        }

        private void placeComponentsFullScreen()
        {
            this.angular_gudge_grid_one.Children.Clear();
            this.angular_gudge_grid_two.Children.Clear();
            this.solid_gudge_grid_one.Children.Clear();
            this.solid_gudge_grid_two.Children.Clear();
            this.solid_gudge_grid_three.Children.Clear();
            this.solid_gudge_grid_four.Children.Clear();
            this.column_chart_grid_one.Children.Clear();
            this.column_chart_grid_two.Children.Clear();
            this.column_chart_grid_three.Children.Clear();
            this.column_chart_grid_four.Children.Clear();
            this.line_chart_grid.Children.Clear();
            this.map_grid.Children.Clear();
            this.gyro_grid.Children.Clear();
            this.home_movement_control_grid.Children.Clear();
            this.home_vision_grid.Children.Clear();
            this.full_screen_movement_control_grid.Children.Clear();
            this.full_screen_vision_grid.Children.Clear();

            this.angular_gudge_grid_one.Children.Add(this._angularGauge1);
            this.angular_gudge_grid_two.Children.Add(this._angularGauge2);
            this.solid_gudge_grid_one.Children.Add(this._solidGauge1);
            this.solid_gudge_grid_two.Children.Add(this._solidGauge2);
            this.solid_gudge_grid_three.Children.Add(this._solidGauge3);
            this.solid_gudge_grid_four.Children.Add(this._solidGauge4);
            this.column_chart_grid_one.Children.Add(this._columnChart1);
            this.column_chart_grid_two.Children.Add(this._columnChart2);
            this.column_chart_grid_three.Children.Add(this._columnChart3);
            this.column_chart_grid_four.Children.Add(this._columnChart4);
            this.line_chart_grid.Children.Add(this._lineChart);
            this.map_grid.Children.Add(this._mapModule);
            this.gyro_grid.Children.Add(this._gyroModule);

            
            this.full_screen_vision_grid.Children.Add(this._visionModule);
            
            this.full_screen_movement_control_grid.Children.Add(this._movementControl);
        }

    }

}
