﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace ParameterScanner
{
    public class Main : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("Parameters");

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            PushButtonData buttonData = new PushButtonData("cmdParameterScanner", "Parameter Scanner", thisAssemblyPath, "ParameterScanner.Command");

            PushButton button = (PushButton)ribbonPanel.AddItem(buttonData);

            button.ToolTip = "Hello there";

            string iconPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "parameter.png");

            Uri icon = new Uri(iconPath, UriKind.Absolute);

            BitmapImage iconImage = new BitmapImage(icon);

            button.LargeImage = iconImage;

            //

            return Result.Succeeded;


        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}