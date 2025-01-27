using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Torsion.Extensions;
using Torsion.Models;
using Torsion.Utils;

namespace Torsion.ViewModels
{
    internal class ViewSchedulePlaceOnMultipleSheetsVM : BaseViewModel
    {
        #region Private Fields
        private string search;
        private ScheduleSheetInstance Instance { get; set; }
        #endregion

        #region Public Properties
        public ObservableCollection<SheetItem> Sheets { get; set; }
        public ICollectionView ItemsView => System.Windows.Data.CollectionViewSource.GetDefaultView(Sheets);
        #endregion

        #region Expanded Properties
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
        {
            try
            {
                //Using a Stopwatch to measure how long the action takes
                Stopwatch sw = Stopwatch.StartNew();
                //Using an int variable to track how many items are affected
                int Count = 0;
                //A Transaction to make changes to the Document
                using(Transaction trans = new Transaction(Doc))
                {
                    sw.Start();
                    trans.Start("Copy Schedules");
                    foreach(SheetItem sheet in Sheets)
                    {
                        if(sheet.IsChecked)
                        {
                            MessageBox.Show(sheet.Name);
                            MoveOrPlace(sheet);
                            MessageBox.Show($"Moved {sheet.Name}");
                            Count++;
                        }
                    }
                    trans.Commit();
                    sw.Stop();
                    UIApp.Application.WriteJournalComment($"Torsion Tools: ScheduleToMultipleSheets [{Count}] {sw.Elapsed.Seconds} seconds", true);
                    Win.DialogResult = true;
                    Win.Close();
                }
            }
            catch(Exception ex)
            {
                ex.Show("Place Schedules");
                Win.DialogResult = false;
                Win.Close();
            }
        }
        private void OnClose()
        {
            Win.Close();
        }
        #endregion

        #region Constructor
        public ViewSchedulePlaceOnMultipleSheetsVM(Window win, Document doc, UIApplication uiapp, ScheduleSheetInstance instance)
        {
            Win = win;
            Doc = doc;
            UIApp = uiapp;
            Instance = instance;
            Title = "Place Schedules";
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
            ItemsView.SortDescriptions.Add(new SortDescription("Number", ListSortDirection.Ascending));
        }
        //Need to check to see if the version of .NET is greater than 8 for the Contain function with StringComparison

#if NET8_0_OR_GREATER
        private bool Filter(SheetItem item)
        {
            return string.IsNullOrWhiteSpace(search)
                || item.Name.Contains(Search, System.StringComparison.CurrentCultureIgnoreCase)
                || item.Number.Contains(Search, System.StringComparison.CurrentCultureIgnoreCase);
        }
#else
        private bool Filter(SheetItem item)
        {
            return string.IsNullOrWhiteSpace(search)
            || item.Name.IndexOf(Search, System.StringComparison.CurrentCultureIgnoreCase) != -1
            || item.Number.IndexOf(Search, System.StringComparison.CurrentCultureIgnoreCase) != -1;
        }
#endif
        private void MoveOrPlace(SheetItem sheet)
        {
            try
            {
                foreach(ScheduleSheetInstance inst in new FilteredElementCollector(Doc, sheet.Sheet.Id).OfClass(typeof(ScheduleSheetInstance)))
                {
                    if(inst.ScheduleId == Instance.ScheduleId)
                    {
                        inst.Point = Instance.Point;
                        return;
                    }
                }
                ScheduleSheetInstance.Create(Doc, sheet.Sheet.Id, Instance.ScheduleId, Instance.Point);
            }
            catch(Exception ex)
            {
                ex.Show("Move or Place");
            }
        }
        #endregion
    }
}