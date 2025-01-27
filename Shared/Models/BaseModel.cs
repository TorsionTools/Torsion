using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PropertyChanged;
using Torsion.Utils;

namespace Torsion.Models
{
    //Using the Interface from Fody.PropertyChanged
    [AddINotifyPropertyChangedInterface]
    public class BaseModel : INotifyPropertyChanged
    {
        #region  Properties
        //Make the IsChecked property available to all classes derived from BaseModel 
        public bool IsChecked { get; set; }
        #endregion

        //This allows the INotifyPropertyChanged interface to be available to all derived Classes
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
