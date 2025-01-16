using System;
using Autodesk.Revit.UI;

namespace Torsion.Extensions
{
  public static class ExceptionExtensions
  {
    /// <summary>
    /// Extension to display a <see cref="TaskDialog"/> for an <see cref="Exception"/>
    /// </summary>
    /// <param name="exception">Try / Catch <see cref="Exception"/></param>
    /// <param name="title"><see cref="string"/> value for the Title of the Task Dialog</param>
    public static void Show(this Exception exception, string title)
    {
      //Intialize a new TaskDialog and pass the title parameter
      using (TaskDialog td = new TaskDialog(title))
      {
        td.Id = exception.GetType().Name;
        td.MainInstruction = exception.Message;
        td.MainContent = exception.StackTrace;
        td.MainIcon = TaskDialogIcon.TaskDialogIconError;
        td.TitleAutoPrefix = true;
        td.FooterText = "Contact us at <a href=\"mailto:tools@torsiontools.com?subject=Support%20Request%20-%20Torsion%20Tools\" target=\"_blank\">tools@torsiontools.com</a> for more help.";
        //This will show the TaskDialog
        td.Show();
      }
    }
  }
}
