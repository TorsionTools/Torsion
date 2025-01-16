using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Torsion.Abstracts;

namespace Torsion.Commands
{
    [Transaction(TransactionMode.Manual)]
    internal class About : ExternalCommand
    {
        internal override void Execute()
        {
            //Initialize a new About form
            Views.About form = new Views.About(CommandData.Application.ActiveUIDocument.Document);

            //Set the Owner of the form to be the Revit Window so it will close / open / stay with Windows depending on usage
            Utils.GetHandle rvtwin = new Utils.GetHandle(CommandData.Application.MainWindowHandle);
            _ = new System.Windows.Interop.WindowInteropHelper(form)
            {
                Owner = rvtwin.Handle
            };

            //Show the Form as modal. This means that is will block all other functionality of Revit when running
            if (form.ShowDialog().Value)
            {
                //Since the default Result.Succeed is set by default in the ExternalCommand base class you can just return here
                return;
            }
            //This will set the Result to Cancelled if the form did not return a true or "OK" value
            Result = Autodesk.Revit.UI.Result.Cancelled;
        }
    }
}
