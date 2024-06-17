using System;
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

            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData("cmdParameterScanner", "Parameter Scanner", assemblyPath, "ParameterScanner.Command");

            PushButton button = (PushButton)ribbonPanel.AddItem(buttonData);
            button.ToolTip = "Parameter Scanner - Only accesible for Floor Plans, Reflected Ceiling Plans and 3D Views";
            BitmapImage iconImage = new BitmapImage(new Uri("pack://application:,,,/ParameterScanner;component/Resources/parametersmall.png"));
            button.LargeImage = iconImage;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
