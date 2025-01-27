using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Torsion.ViewModels;

namespace Torsion.Views
{
    public sealed partial class ViewSchedulePlaceOnMultipleSheetsView : Window
    {
        private ViewSchedulePlaceOnMultipleSheetsVM VM;
        public ViewSchedulePlaceOnMultipleSheetsView(Document doc, UIApplication uiapp, ScheduleSheetInstance instance)
        {
            this.InitializeComponent();
            VM = new ViewSchedulePlaceOnMultipleSheetsVM(this, doc, uiapp, instance);
            DataContext = VM;
        }
    }
}