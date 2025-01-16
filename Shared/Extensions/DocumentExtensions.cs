using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Torsion.Extensions
{
    /// <summary>
    /// Extension Class for <see cref="Autodesk.Revit.DB.Document"/>
    /// </summary>
    public static class DocumentExtensions
    {
        /// <summary>
        /// Element Collector for the <see cref="Autodesk.Revit.DB.BuiltInCategory"/> specified.
        /// </summary>
        /// <param name="document"><see cref="Document"/></param>
        /// <param name="category"><see cref="Autodesk.Revit.DB.BuiltInCategory"/></param>
        /// <returns><see cref="List{T}"/> of <see cref="Autodesk.Revit.DB.Element"/></returns>
        /// <remarks>May return <see cref="Autodesk.Revit.DB.ElementType"/></remarks>
        public static IList<Element> OfCategory(this Document document, BuiltInCategory category)
        {
            using(FilteredElementCollector collector = new FilteredElementCollector(document).OfCategory(category))
            {
                return collector.ToElements();
            }
        }
        /// <summary>
        /// Element Collector of Element Types for the <see cref="Autodesk.Revit.DB.BuiltInCategory"/> specified.
        /// </summary>
        /// <param name="document"><see cref="Document"/></param>
        /// <param name="category"><see cref="Autodesk.Revit.DB.BuiltInCategory"/></param>
        /// <returns><see cref="List{T}"/> of <see cref="Autodesk.Revit.DB.ElementType"/></returns>
        /// <remarks>Only returns <see cref="Autodesk.Revit.DB.ElementType"/></remarks>
        public static IList<Element> OfCategoryType(this Document document, BuiltInCategory category)
        {
            using(FilteredElementCollector collector = new FilteredElementCollector(document).OfCategory(category).WhereElementIsElementType())
            {
                return collector.ToElements();
            }
        }
#if NET8_0_OR_GREATER
        /// <summary>
        /// Retrieve a <see cref="ViewSheet"/> with the specified Sheet Number and Sheet Collection Name
        /// </summary>
        /// <param name="document">Active <see cref="Document"/></param>
        /// <param name="sheetNumber">Sheet Number as <see langword="string"/></param>
        /// <param name="collectionId">Sheet Collection <see cref="Autodesk.Revit.DB.ElementId"/></param>
        /// <returns><see cref="ViewSheet"/> if found, <see langword="null"/> otherwise</returns>
        public static ViewSheet GetSheetByNumber(this Document document, string sheetNumber, ElementId collectionId)
        {
            ParameterValueProvider pvpNumber = new ParameterValueProvider(new ElementId(BuiltInParameter.SHEET_NUMBER));
            FilterStringRuleEvaluator evalNumber = new FilterStringEquals();
            FilterRule collectionRule = ParameterFilterRuleFactory.CreateEqualsRule(new ElementId(BuiltInParameter.SHEET_COLLECTION), collectionId);
            FilterRule numberRule = new FilterStringRule(pvpNumber, evalNumber, sheetNumber);
            ElementParameterFilter numberFilter = new ElementParameterFilter(numberRule);
            ElementParameterFilter collectionFilter = new ElementParameterFilter(collectionRule);
            LogicalAndFilter filter = new LogicalAndFilter(numberFilter, collectionFilter);

            if(new FilteredElementCollector(document).OfClass(typeof(ViewSheet)).WherePasses(filter).FirstOrDefault() is ViewSheet sheet)
            {
                return sheet;
            }
            return null;
        }
#else
        public static ViewSheet GetSheetByNumber(this Document document, string sheetNumber)
        {
            ParameterValueProvider pvpNumber = new ParameterValueProvider(new ElementId(BuiltInParameter.SHEET_NUMBER));
            FilterStringRuleEvaluator evalNumber = new FilterStringEquals();
            FilterRule numberRule = new FilterStringRule(pvpNumber, evalNumber, sheetNumber);
            ElementParameterFilter numberFilter = new ElementParameterFilter(numberRule);

            if(new FilteredElementCollector(document).OfClass(typeof(ViewSheet)).WherePasses(numberFilter).FirstOrDefault() is ViewSheet sheet)
            {
                return sheet;
            }
            return null;
        }
#endif
    }
}
