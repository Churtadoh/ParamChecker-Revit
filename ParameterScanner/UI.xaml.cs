﻿using System;
using System.Collections.Generic;
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
using Autodesk.Revit.DB;

namespace ParameterScanner
{
    /// <summary>
    /// Interaction logic for UI.xaml
    /// </summary>
    public partial class UI : Window
    {
        Document _doc;
        public UI(Document doc)
        {
            InitializeComponent();
            _doc = doc;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) 
        {
            
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}