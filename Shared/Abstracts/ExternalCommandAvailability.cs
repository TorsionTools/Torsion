using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Torsion.Extensions;

namespace Torsion.Abstracts
{
    /// <summary>
    /// Base Class for External Command Availability classes for Ribbon Buttons
    /// </summary>
    internal abstract class CommandAvailability : IExternalCommandAvailability
    {
        //Access to the ActiveUIDocument from the applicationData in the Override Class
        internal Document Doc { get; private set; } = default;
        //By default the IsCommandAvailable will check that a Document is Active.
        //Manually set this with the CheckDocument method below if a Non-Document state is required
        private bool CheckDocumentOpen { get; set; } = true;

        /// <summary>
        /// Implement this method to provide control over whether your external command is enabled or disabled.
        /// </summary>
        /// <param name="applicationData"><see cref="Element"/></param>
        /// <param name="selectedCategories"></param>
        /// <returns></returns>
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            try
            {
                //This check will make sure a valid Document is open
                if (CheckDocumentOpen)
                {
                    if (applicationData.ActiveUIDocument == null)
                    {
                        return false;
                    }
                }
                //Set the Doc property to the ActiveUIDocument for access in the Override Class 
                this.Doc = applicationData.ActiveUIDocument.Document;
            }
            catch (Exception ex)
            {
                //Use the Exception Extension class to display the Exception information
                ex.Show("Command Availability");
            }
            //Returns the bool value from the overriden abtract class.
            return SetCommandAvailability(applicationData, selectedCategories);
        }
        /// <summary>
        /// Use this Method to set the CheckDocumentOpen property to allow a command in a non-document state
        /// </summary>
        /// <param name="check"></param>
        public virtual void CheckDocument(bool check)
        {
            CheckDocumentOpen = check;
        }
        /// <summary>
        /// This is the abstract class that will be overriden in the main ExternalCommandAvailability classes
        /// </summary>
        /// <param name="applicationData"></param>
        /// <param name="selectedCategories"></param>
        /// <returns><see cref="bool"/></returns>
        public abstract bool SetCommandAvailability(UIApplication applicationData, CategorySet selectedCategories);
    }
}
