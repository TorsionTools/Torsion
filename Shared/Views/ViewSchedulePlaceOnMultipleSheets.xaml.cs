using System.Windows;
using Autodesk.Revit.DB;
using Torsion.ViewModels;

namespace Torsion.Views
{
    public sealed partial class ViewSchedulePlaceOnMultipleSheetsView : Window
    {
        private ViewSchedulePlaceOnMultipleSheetsVM VM;
        public ViewSchedulePlaceOnMultipleSheetsView(Document doc)
        {
            this.InitializeComponent();
            VM = new ViewSchedulePlaceOnMultipleSheetsVM(this, doc);
            DataContext = VM;
        }
    }
}