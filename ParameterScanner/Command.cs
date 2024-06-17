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

                //Add more views if neccessary
                List<ViewType> allowedViews = new List<ViewType>
                {
                    ViewType.FloorPlan,
                    ViewType.CeilingPlan,
                    ViewType.ThreeD
                };

                View currentView = doc.ActiveView;

                if (currentView == null || !allowedViews.Contains(currentView.ViewType)) 
                {
                    MessageBox.Show("The Parameter Scanner is only available for Floor Plans, Reflected Ceiling Plans, or 3D Views.", "Warning", MessageBoxButton.OK,MessageBoxImage.Warning);
                    return Result.Cancelled;
                }

                UI mainWindow = new UI(doc);
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
