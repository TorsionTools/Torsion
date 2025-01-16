using Autodesk.Revit.DB;

namespace Torsion.Extensions
{
    /// <summary>
    /// Extension Class for <see cref="Autodesk.Revit.DB.ElementId"/>
    /// </summary>
    public static class ElementIdExtensions
    {
        /// <summary>
        /// Retrieves <see cref="Autodesk.Revit.DB.Element"/> from <see cref="Autodesk.Revit.DB.ElementId"/>
        /// </summary>
        /// <param name="elementId"><see cref="Autodesk.Revit.DB.ElementId"/></param>
        /// <param name="document"><see cref="Document"/></param>
        /// <returns><see cref="Autodesk.Revit.DB.Element"/></returns>
        public static Element ToElement(this ElementId elementId, Document document)
        {
            return document.GetElement(elementId);
        }
        /// <summary>
        /// Retrieves <see cref="Autodesk.Revit.DB.Element"/> from <see cref="Autodesk.Revit.DB.ElementId"/>
        /// </summary>
        /// <typeparam name="T"><see cref="Autodesk.Revit.DB.Element"/> <see langword="class"/> to cast</typeparam>
        /// <param name="elementId"><see cref="Autodesk.Revit.DB.ElementId"/></param>
        /// <param name="document"><see cref="Document"/></param>
        /// <returns><see cref="Autodesk.Revit.DB.Element"/> of <see cref="class"/></returns>
        public static T ToElement<T>(this ElementId elementId, Document document) where T : Element
        {
            return document.GetElement(elementId) as T;
        }
    }
}