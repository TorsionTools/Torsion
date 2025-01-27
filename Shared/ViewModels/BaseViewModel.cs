using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PropertyChanged;
using Torsion.Utils;

namespace Torsion.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    internal class BaseViewModel : INotifyPropertyChanged
    {
        #region  Properties
        internal System.Windows.Window Win { get; set; }
        internal Autodesk.Revit.DB.Document Doc { get; set; }
        internal Autodesk.Revit.UI.UIApplication UIApp { get; set; }
        public static string Version => AppVars.Version;
        public string Title { get; set; }
        public string Icon { get; set; } = $"pack://application:,,,/{AppVars.AssemblyName};component/Images/Icon.ico";
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Command Helpers
        protected static async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            if(updatingFlag.GetPropertyValue())
            {
                return;
            }

            updatingFlag.SetPropertyValue(true);

            try
            {
                await action();
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }
        }
        #endregion
    }
}
