using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using Torsion.Utils;

namespace Torsion.ViewModels
{
    internal class AboutVM : BaseViewModel
    {
        #region Private Properties
        #endregion

        #region Public Properties
        public string AppName { get; set; } = "Torsion Tools";
        public string PublisherName { get; set; } = "Sean Page";
        public string Build { get; set; }
        public string RevitVersion { get; set; }
        public string URLName { get; set; } = "TorsionTools.com";
        public string URL { get; set; } = @"http://www.TorsionTools.com";
        #endregion

        #region Commands
        public ICommand CloseCommand { get; set; }
        #endregion

        #region Command Methods
        private void OnClose()
        {
            Win.Close();
        }
        #endregion

        #region Constructor
        public AboutVM(Window win, Document doc)
        {
            //Reference to the Window so you can close it
            Win = win;
            //Reference to the Document passed from the Command
            Doc = doc;
            //Window Title binding for the Title property of the View
            Title = $"About {Build}";
            //Set the ICommands to the Method with RelayCommand
            CloseCommand = new RelayCommand(OnClose);
            //Set Properties
            Build = AppVars.Version;
            RevitVersion = doc.Application.VersionNumber;
        }
        #endregion
    }
}
