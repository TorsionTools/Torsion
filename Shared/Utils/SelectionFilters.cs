using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace Torsion.Utils
{
    internal class SelectionFilters
    {
        internal class ScheduleSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element element)
            {
                return element.Category.BuiltInCategory == BuiltInCategory.OST_ScheduleGraphics;
            }

            public bool AllowReference(Reference refer, XYZ point)
            {
                return false;
            }
        }
    }
}
