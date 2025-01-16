using System;
using Autodesk.Revit.DB;
using Torsion.Extensions;

namespace Torsion.Updaters
{/// <summary>
 /// This class is for Dynamic Model  Updating (DMU) that can manipulate newly created elements that pass the filters st for them
 /// </summary>
    internal class ViewSheetUpdater : IUpdater
    {
        //Create the static variables needed to Register and Enable the Updaters
        static AddInId applicationId;
        static UpdaterId updaterId;

        /// <summary>
        /// Use this Method to Initialize and return the UpdaterId for registration and Enabling / Disabling the updater
        /// The GUID here is unique to this updater and should be regenerated for any additional Updaters
        /// </summary>
        /// <param name="appId"></param>
        public ViewSheetUpdater(AddInId appId)
        {
            applicationId = appId;
            updaterId = new UpdaterId(appId, new Guid("{43E53779-B69B-45D0-9738-10849182A182}"));
        }

        /// <summary>
        /// This is where all of the work is done, and the Data is the information supplied by Revit when new elemnts are created
        /// </summary>
        /// <param name="data"></param>
        public void Execute(UpdaterData data)
        {
            //Get the current Document
            Document Doc = data.GetDocument();
            //Check to make sure the Current Document is not a Family Document
            if(Doc.IsFamilyDocument)
            {
                return;
            }
            //You can Cycle through the AddedElementIds or the Deleted or Modified 
            //Other possible options are: data.GetDeletedElementIds, data.GetModifiedElementIds
            foreach(ElementId addedElemId in data.GetAddedElementIds())
            {
                //Cast the ElmentIds to the type of element you are working with. In this case they are Sheets 
                ViewSheet sheet = addedElemId.ToElement<ViewSheet>(Doc);
                //This form will ask the User to Input the Name and Number of the New sheet during creation
                //using (Forms.ViewSheetUpdaterForm form = new Forms.ViewSheetUpdaterForm(Doc, sheet))
                //{
                //  //Show the form as a modal dialog. This means that it is "Owned" by the Main Window in Revit and is the thing active
                //  //This is considered Thread Blocking meaning you can't do anything else in Revit while this Dialog is showing
                //  form.ShowDialog();
                //}
            }
        }

        /// <summary>
        /// These attitional methods are all required for each updater
        /// This can be quereyed for Task dialogs or error messages
        /// </summary>
        /// <returns></returns>
        public string GetAdditionalInformation()
        {
            return "Check to see when Sheets are added";
        }

        /// <summary>
        /// Set the ChangePriority of the Updater so that it will narrow the items it is "watching"
        /// </summary>
        /// <returns></returns>
        public ChangePriority GetChangePriority()
        {
            return ChangePriority.Views;
        }

        /// <summary>
        /// Used to Retrieve the Static UpdaterId
        /// </summary>
        /// <returns></returns>
        public UpdaterId GetUpdaterId()
        {
            return updaterId;
        }

        /// <summary>
        /// Sets the Updater Name to be called or shown for Task Dialogs
        /// </summary>
        /// <returns></returns>
        public string GetUpdaterName()
        {
            return "Torsion Tools Sheet Information Updater";
        }
    }
}
