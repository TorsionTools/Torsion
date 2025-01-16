using System.Reflection;
using Autodesk.Revit.UI;
using Torsion.Commands;
using Torsion.Extensions;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;

namespace Torsion.Ribbon
{
    class AppRibbon
    {
        internal static void AddPanel(UIControlledApplication RevitApplication)
        {
            //Tab Name that will display in Revit
            string TabName = "Torsion Tools";

            //Create the Ribbon Tab
            RevitApplication.CreateRibbonTab(TabName);

            //Get the assembly path to execute commands
            string AssemblyPath = Assembly.GetExecutingAssembly().Location;

            //Create a Panel within the Tab
            RibbonPanel RibbonPanelOne = RevitApplication.CreateRibbonPanel(TabName, "PANEL 1");
            RibbonPanel RibbonPanelSheets = RevitApplication.CreateRibbonPanel(TabName, "Sheets");
            RibbonPanel RibbonPanelViews = RevitApplication.CreateRibbonPanel(TabName, "Views");
            RibbonPanel RibbonPanelTools = RevitApplication.CreateRibbonPanel(TabName, "Tools");
            RibbonPanel RibbonPanelMEP = RevitApplication.CreateRibbonPanel(TabName, "MEP");
            RibbonPanel RibbonPanelSettings = RevitApplication.CreateRibbonPanel(TabName, "Settings");

            #region Panel 1
            //Create a Push Button from the Push Button Data using Ppush Button Extensions
            RibbonPanelOne.AddPushButton<HelloWorld>("Button Name")
              .SetAvailabilityClass<DocumentActive>()
              .SetToolTip("Tell the user what your button does here")
              .SetLongDescription("Give the user more information about how they need to use the button features")
              .SetLargeImage("Button100x100.png");
            #endregion

            #region Tools
            RibbonPanelTools.AddPushButton<RevitLinksReloadAll>("Reload Links")
                .SetAvailabilityClass<DocumentActive>()
                .SetToolTip("Reloads all currently loaded Revit Link Types")
                .SetLargeImage("Reload.png");
            #endregion

            #region Settings
            RibbonPanelSettings.AddPushButton<About>("About")
                .SetAvailabilityClass<DocumentActive>()
                .SetToolTip("Information about this application")
                .SetLargeImage("SquareT100x100.png");
            #endregion
        }
    }
}