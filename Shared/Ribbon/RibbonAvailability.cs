using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Torsion.Abstracts;

namespace Torsion.Ribbon
{
    /// <summary>
    /// Button will be Visible if there is an Active Document (Model) Open
    /// </summary>
    class DocumentActive : CommandAvailability
    {
        public override bool SetCommandAvailability(UIApplication applicationData, CategorySet selectedCategories)
        {
            return applicationData.ActiveUIDocument != null;
        }
    }
}
