using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Torsion.Abstracts;
using Torsion.Extensions;
using Torsion.Utils;

namespace Torsion.Commands
{
    [Transaction(TransactionMode.Manual)]
    internal class RevitLinksReloadAll : ExternalCommand
    {
        internal override void Execute()
        {
            //Get all Revit Links
            using(FilteredElementCollector rvtLinks = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_RvtLinks).OfClass(typeof(RevitLinkType)))
            {
                //Loop each Revit Link
                foreach(RevitLinkType rvtLink in rvtLinks.ToElements())
                {
                    try
                    {
                        //Check to see if the Link is Loaded
                        if(rvtLink.GetLinkedFileStatus() == LinkedFileStatus.Loaded)
                        {
                            //Reload the Link if Loaded
                            rvtLink.Reload();
                        }
                    }
                    catch(Exception ex)
                    {
                        //This exception is thrown when the Link is open in another document and cannot be reloaded
                        if(ex.GetType() == typeof(InvalidOperationException))
                        {
                            Dialog.Information("Torsion_Link_Open", "Document Open", ex.Message, $"{rvtLink.Name} is currently open and cannot be reloaded.");
                            continue;
                        }
                        //Show any other exceptions using the exception extension
                        ex.Show("Reload All Links");
                        //Set the Result property on the Abstract ExternalCommand class
                        Result = Autodesk.Revit.UI.Result.Failed;
                    }
                }
            }
        }
    }
}
