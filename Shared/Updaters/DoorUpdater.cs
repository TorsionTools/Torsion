using System;
using Autodesk.Revit.DB;
using Torsion.Extensions;

namespace Torsion.Updaters
{
    internal class DoorUpdater : IUpdater
    {
        //Create the static variables needed to Register and Enable the Updaters
        static AddInId applicationId;
        static UpdaterId updaterId;

        /// <summary>
        /// Use this Method to Initialize and return the UpdaterId for registration and Enabling / Disabling the updater
        /// The GUID here is unique to this updater and should be regenerated for any additional Updaters
        /// </summary>
        /// <param name="appId"><see cref="Autodesk.Revit.DB.AddInId"/></param>
        public DoorUpdater(AddInId appId)
        {
            applicationId = appId;
            updaterId = new UpdaterId(applicationId, new Guid("{F2466DFD-9E78-45E9-87ED-7E1C59EB163E}"));
        }

        public void Execute(UpdaterData data)
        {
            foreach(ElementId addedElemId in data.GetAddedElementIds())
            {
                //Do SOmething with New Doors 
            }

            foreach(ElementId modifiedElemId in data.GetModifiedElementIds())
            {
                if(data.IsChangeTriggered(modifiedElemId, Element.GetChangeTypeGeometry()))
                {
                    if(modifiedElemId.ToElement<FamilyInstance>(data.GetDocument()) is FamilyInstance door)
                    {
                        //Do something with the door that had it's geometry changed
                    }
                }
            }
        }

        public string GetAdditionalInformation()
        {
            return "This Updater will update the Doors in the Model";
        }

        public ChangePriority GetChangePriority()
        {
            return ChangePriority.DoorsOpeningsWindows;
        }

        public UpdaterId GetUpdaterId()
        {
            return updaterId;
        }

        public string GetUpdaterName()
        {
            return "Torsion Tools Door Updater";
        }
    }
}
