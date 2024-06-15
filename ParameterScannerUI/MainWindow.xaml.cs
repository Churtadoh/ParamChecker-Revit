using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ParameterScannerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UIDocument _uiDoc;
        private readonly Document _doc;
        public MainWindow(UIDocument uiDoc)
        {
            InitializeComponent();
            _uiDoc = uiDoc;
            _doc = uiDoc.Document;
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            // Example of scanning elements
            var collector = new FilteredElementCollector(_doc);
            var elements = collector.WhereElementIsNotElementType()
                                    .Where(x => x.LookupParameter("YourParameterName") != null)
                                    .ToList();

            foreach (var element in elements)
            {
                var paramValue = element.LookupParameter("YourParameterName").AsString();
                // Do something with paramValue
            }

            MessageBox.Show($"{elements.Count} elements scanned.");
        }

    }
}