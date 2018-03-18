using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using arcEye_2k18.controllers;
using arcEye_2k18.dataTemplate;

namespace arcEye_2k18
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        keyRegistrar _keyRegistrar = new keyRegistrar();
        dataPublisher _dataPublisher = new dataPublisher();
    }
}
