using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using Torsion.Utils;
using Torsion.Models;
using Torsion.Extensions;
using System.Collections.Generic;

namespace Torsion.ViewModels
{
    internal class ViewSchedulePlaceOnMultipleSheetsVM : BaseViewModel
    {
        #region Private Properties
        private string search;
        #endregion

        #region Public Properties
        public ObservableCollection<SheetItem> Sheets { get; set; }
        public ICollectionView ItemsView => System.Windows.Data.CollectionViewSource.GetDefaultView(Sheets);
        #endregion

        #region Expanded
        public string Search
        {
            get
            {
                return search;
            }
            set
            {
                search = value;
                OnPropertyChanged(nameof(Search));
                ItemsView.Refresh();
            }
        }
        #endregion

        #region Commands
        public ICommand OkCMD { get; set; }
        public ICommand CloseCMD { get; set; }

        private void OnOK()
        { }
        private void OnClose()
        {
            Win.Close();
        }
        #endregion

        #region Constructor
        public ViewSchedulePlaceOnMultipleSheetsVM(Window win, Document doc)
        {
            Win = win;
            Doc = doc;
            Title = $" {AppVars.Version}";
            OkCMD = new RelayCommand(OnOK);
            CloseCMD = new RelayCommand(OnClose);
            GetSheets(doc);
        }
        #endregion

        #region Methods
        private void GetSheets(Document doc)
        {
            //Create a temporary list to hold the elements and initialize it
            List<SheetItem> temp = new List<SheetItem>();
            //Use the Filtered Element Collector extention to collect ViewSheets
            foreach(ViewSheet sheet in Doc.OfCategory(BuiltInCategory.OST_Sheets))
            {
                //Using ViewSheet create new instances of the SheetItem class and add them to the list
                temp.Add(new SheetItem(sheet));
            }
            //Use the temp list to intialize the Sheets Observerable Collection. 
            //You avoid a possible null collection here because temp above is intialize with an empty list
            Sheets = new ObservableCollection<SheetItem>(temp);
            //Add SheetItem Observerable Collection Filter and Sorting
            ItemsView.Filter = new System.Predicate<object>(sheet => Filter(sheet as SheetItem));
            ItemsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }
        //Need to check to see if the version of .NET is greater than 8 for the Contain function with StringComparison
#if NET8_0_OR_GREATER
        private bool Filter(SheetItem item)
        {
            return string.IsNullOrWhiteSpace(search) 
                || item.Name.Contains(Search,System.StringComparison.CurrentCultureIgnoreCase)
                || item.Number.Contains(Search, System.StringComparison.CurrentCultureIgnoreCase);
        }
#else
        private bool Filter(SheetItem item)
        {
            return string.IsNullOrWhiteSpace(search) 
            || item.Name.IndexOf(Search,System.StringComparison.CurrentCultureIgnoreCase) != -1
            || item.Number.IndexOf(Search,System.StringComparison.CurrentCultureIgnoreCase) != -1;
        }
#endif
        #endregion
    }
}