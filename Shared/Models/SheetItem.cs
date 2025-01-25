using Autodesk.Revit.DB;

namespace Torsion.Models
{
    public class SheetItem : BaseModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public ViewSheet Sheet { get; set; }
        public SheetItem(ViewSheet sheet) 
        { 
            Name = sheet.Name;
            Number = sheet.SheetNumber;
            Sheet = sheet;
        }
    }
}
