using Torsion.Abstracts;
using Torsion.Utils;

namespace Torsion.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class HelloWorld : ExternalCommand
    {
        internal override void Execute()
        {
            Dialog.Information("Torsion_Dialog_Id", "Testing", "This is the Large Primary Text", "This is the smaller text that appears under the primary text. This can be used for further instructions.");
        }
    }
}