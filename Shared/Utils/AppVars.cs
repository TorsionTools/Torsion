using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Autodesk.Revit.UI;
using Torsion.Models;

namespace Torsion.Utils
{
  /// <summary>
  /// Class to provide Application Wide properties and fields
  /// </summary>
  internal class AppVars
  {
    /// <summary>
    /// A property to hold the UI Controlled Application in
    /// </summary>
    public static UIControlledApplication uiControlledApp { get; set; }
    /// <summary>
    /// A property to hold the UI Application in
    /// </summary>
    public static UIApplication uiApplication { get; set; }
    /// <summary>
    /// A list of custom MapParam class objects to store the Parameter Mappings
    /// </summary>
    internal static List<MappedParameter> ParameterMappings { get; set; } = default;
    /// <summary>
    /// The current version of the executing assembly to append to Window Titles
    /// </summary>
    internal static string Version => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
    /// <summary>
    /// The Name of the currently executing assembly for use in Application packing like images or files
    /// </summary>
    internal static string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
  }
}
