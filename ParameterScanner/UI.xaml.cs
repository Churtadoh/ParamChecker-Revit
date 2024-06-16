using System;
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

        private void IsolateViewClick(object sender, RoutedEventArgs e) 
        {
            SearchInput searchInput = new SearchInput()
            {
                ParameterName = ParameterName.Text,
                ParameterValue = ParameterValue.Text,
            };

            List<Element> elements = FindElements(_doc, searchInput);

            if(elements != null && elements.Count > 0)
            {
                using (Transaction tx = new Transaction(_doc, "Isolate Elements"))
                {
                    tx.Start();
                    _doc.ActiveView.IsolateElementsTemporary(elements.Select(ele => ele.Id).ToList());
                    tx.Commit();
                }
            }
            else
            {
                MessageBox.Show("No elements found.");
            }
        }

        private void SelectClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{ParameterName.Text} - {ParameterValue.Text}");
        }

        public class SearchInput
        {
            public string ParameterName { get; set; }
            public string ParameterValue { get; set; }
        }

        public static List<Element> FindElements(Document doc, SearchInput input)
        {
            try
            {
                List<Element> elements = null;

                FilteredElementCollector collector = new FilteredElementCollector(doc).WhereElementIsNotElementType();

                foreach (Element element in collector)
                {
                    Parameter param = element.LookupParameter(input.ParameterName);

                    if (param != null)
                    {
                        if (elements == null)
                        {
                            elements = new List<Element>();
                        }

                        if (string.IsNullOrEmpty(input.ParameterValue))
                        {

                            elements.Add(element);

                        }
                        else if (param.AsString() == input.ParameterValue)
                        {
                            elements.Add(element);
                        }
                    }
                }

                return elements;

            } catch
            {
                throw;
            }
        }
    }
}
