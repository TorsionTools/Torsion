using Autodesk.Revit.UI;

namespace Torsion.Utils
{
    public static class Dialog
    {
        /// <summary>
        /// Creates and Shows an Error <see cref="TaskDialog"/>
        /// </summary>
        /// <param name="id">The Id of the Task Dialog</param>
        /// <param name="title">Dialog Title</param>
        /// <param name="message">The large primary text that appears at the top of the Dialog</param>
        /// <param name="directions">The smaller text that appears right below the primary text</param>
        public static void Error(string id, string title, string message, string directions)
        {
            using(TaskDialog td = new TaskDialog(title))
            {
                td.Id = id;
                td.MainInstruction = message;
                td.MainContent = directions;
                td.TitleAutoPrefix = true;
                td.MainIcon = TaskDialogIcon.TaskDialogIconError;
                td.CommonButtons = TaskDialogCommonButtons.Cancel;
                td.FooterText = AppVars.Version;
                td.Show();
            }
        }
        /// <summary>
        /// Creates and Shows an Informational <see cref="TaskDialog"/>
        /// </summary>
        /// <param name="id">The Id of the Task Dialog</param>
        /// <param name="title">Dialog Title</param>
        /// <param name="message">The large primary text that appears at the top of the Dialog</param>
        /// <param name="directions">The smaller text that appears right below the primary text</param>
        public static void Information(string id, string title, string message, string directions)
        {
            using(TaskDialog td = new TaskDialog(title))
            {
                td.Id = id;
                td.MainInstruction = message;
                td.MainContent = directions;
                td.TitleAutoPrefix = true;
                td.MainIcon = TaskDialogIcon.TaskDialogIconInformation;
                td.CommonButtons = TaskDialogCommonButtons.Ok;
                td.FooterText = AppVars.Version;
                td.Show();
            }
        }
    }
}
