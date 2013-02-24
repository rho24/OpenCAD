using System.Windows;
using System.Windows.Controls;

namespace OpenCAD.GUI.Infrastructure
{
    public class AutobinderTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Template { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return Template;
        }
    }
}