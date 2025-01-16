using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Torsion.Extensions;

namespace Torsion.Abstracts
{
    /// <summary>
    /// Base Class for External Command classes for Revit API Commands
    /// </summary>
    public abstract class ExternalCommand : IExternalCommand
    {
        /// <summary>
        /// A class contains reference to Application and View which are needed by external command
        /// </summary>
        internal ExternalCommandData CommandData { get; private set; } = default;
        /// <summary>
        /// Error Message can be returned by External Command
        /// </summary>
        internal string Message { get; set; } = string.Empty;
        /// <summary>
        /// Element set indicating problem elements to display in the failure dialog. This will be used only if the command status was "Failed"
        /// </summary>
        internal ElementSet Elements { get; private set; } = default;
        /// <summary>
        /// This sets the default Result to Succeeded so you don't need to manually set this unless you need a difderent value
        /// </summary>
        internal Result Result { get; set; } = Result.Succeeded;
        /// <summary>
        /// Provide the Document and UIApplicaiton to the Override Methods
        /// </summary>
        internal Document Doc { get; private set; }
        internal UIApplication UIApp { get; private set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                //Set the Properties to pass the information to the Override
                this.Elements = elements;
                this.Message = message;
                this.CommandData = commandData;
                this.UIApp = commandData.Application;
                this.Doc = commandData.Application.ActiveUIDocument.Document;

                //Call the Override Execute() method
                Execute();
            }
            catch (Exception ex)
            {
                //Set the default result to Failed when there is an Exception caught
                Result = Result.Failed;
                ex.Show("External Command");
            }
            finally
            {
                //Update the Message in case there were any errors
                message = this.Message;
            }
            //Return the Result of the Command
            return this.Result;
        }
        /// <summary>
        /// This method will be the one overriden when another Class is derived from this one.
        /// </summary>
        internal abstract void Execute();
    }
}
