using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterScanner
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIApplication application = commandData.Application;
                Document doc = application.ActiveUIDocument.Document;

                var mainWindow = new UI(doc);
                mainWindow.ShowDialog();
                return Result.Succeeded;
            }
            catch (Exception ex) 
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
