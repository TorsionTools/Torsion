using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Torsion.Extensions;
using Torsion.Utils;

namespace Torsion.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    internal class ViewSchedulePlaceOnMultipleSheets : Abstracts.ExternalCommand
    {
        internal override void Execute()
        {
            if(Doc.GetElement(Doc.PickElement<SelectionFilters.ScheduleSelectionFilter>("Select a Schedule on a Sheet")) is ScheduleSheetInstance instance)
            {
                Views.ViewSchedulePlaceOnMultipleSheetsView form = new Views.ViewSchedulePlaceOnMultipleSheetsView(Doc, UIApp, instance);
                Utils.GetHandle rvtwin = new Utils.GetHandle(CommandData.Application.MainWindowHandle);
                _ = new System.Windows.Interop.WindowInteropHelper(form)
                {
                    Owner = rvtwin.Handle
                };

                if(form.ShowDialog().Value)
                {
                    return;
                }
                else
                {
                    Result = Result.Cancelled;
                }
            }
            Result = Result.Cancelled;
        }
    }
}
