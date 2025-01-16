using System.Windows;
using Torsion.ViewModels;

namespace Torsion.Views
{
    public sealed partial class About : Window
    {
        AboutVM VM { get; set; }
        public About(Autodesk.Revit.DB.Document Doc)
        {
            this.InitializeComponent();
            DataContext = new AboutVM(this, Doc);
        }
    }
}