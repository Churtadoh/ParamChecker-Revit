using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ParameterScanner.Models;

namespace ParameterScanner
{
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
            try
            {
                if (_doc.ActiveView.IsInTemporaryViewMode(TemporaryViewMode.TemporaryHideIsolate))
                {
                    using (Transaction tx = new Transaction(_doc, "Reset Isolation"))
                    {
                        tx.Start();
                        _doc.ActiveView.DisableTemporaryViewMode(TemporaryViewMode.TemporaryHideIsolate);
                        tx.Commit();
                    }
                }


                SearchInput searchInput = new SearchInput(ParameterName.Text, ParameterValue.Text);

                List<Element> elements = FindElements(_doc, searchInput);

                if (elements != null && elements.Count > 0)
                {
                    using (Transaction tx = new Transaction(_doc, "Isolate Elements"))
                    {
                        tx.Start();
                        _doc.ActiveView.IsolateElementsTemporary(elements.Select(ele => ele.Id).ToList());
                        tx.Commit();
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

       

        private void SelectClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchInput searchInput = new SearchInput(ParameterName.Text, ParameterValue.Text);

                List<Element> elements = FindElements(_doc, searchInput);

                if (elements != null && elements.Count > 0)
                {
                    UIDocument uidoc = new UIDocument(_doc);
                    uidoc.Selection.SetElementIds(elements.Select(ele => ele.Id).ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private static List<Element> FindElements(Document doc, SearchInput input)
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
                        else
                        {
                            if (param.StorageType == StorageType.String)
                            {
                                if (param.AsString() == input.ParameterValue)
                                {
                                    elements.Add(element);
                                }
                            }
                            else if (param.StorageType == StorageType.Integer)
                            {
                                if (int.TryParse(input.ParameterValue, out int value) && param.AsInteger() == value)
                                {
                                    elements.Add(element);
                                }
                            }
                            else if (param.StorageType == StorageType.Double)
                            {
                                double.TryParse(input.ParameterValue, out double value);

                                double paramValue = UnitUtils.ConvertFromInternalUnits(param.AsDouble(), param.DisplayUnitType);

                                if (Math.Round(paramValue, 3) == Math.Round(value, 3))
                                {
                                    elements.Add(element);
                                }
                            }
                            else if (param.StorageType == StorageType.ElementId)
                            {
                                ElementId elementId = param.AsElementId();
                                Element referencedElement = doc.GetElement(elementId);

                                string referencedElementName = referencedElement?.Name;

                                if (referencedElementName == input.ParameterValue)
                                {
                                            
                                    elements.Add(element);
                                }
                            }
                        }
                    }
                }
                if (elements != null && elements.Count > 0) 
                {
                    MessageBox.Show($"Found {elements.Count} elements for the provided inputs.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    DisplayWarningMessage(input);
                }

                return elements;

            } catch
            {
                throw;
            }
        }

        private static void DisplayWarningMessage(SearchInput searchInput)
        {
            string parameterNameMessage = $" with the specified parameter name '{searchInput.ParameterName}'";
            string parameterValueMessage = string.IsNullOrEmpty(searchInput.ParameterValue) ? "" : $" and parameter value '{searchInput.ParameterValue}'";

            MessageBox.Show($"No elements found{parameterNameMessage}{parameterValueMessage}.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
