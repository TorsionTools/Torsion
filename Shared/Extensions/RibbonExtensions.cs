using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Torsion.Utils;

namespace Torsion.Extensions
{
  public static class RibbonExtensions
  {
    /// <summary>
    /// Sets the Image of the PushButton
    /// </summary>
    /// <param name="button"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static RibbonButton SetImage(this RibbonButton button, string name)
    {
      button.Image = new BitmapImage(new Uri($"pack://application:,,,/{AppVars.AssemblyName};component/Images/{name}", UriKind.RelativeOrAbsolute));
      return button;
    }
    /// <summary>
    /// Sets the Large Image of the PushButton
    /// </summary>
    /// <param name="button"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static RibbonButton SetLargeImage(this RibbonButton button, string name)
    {
      button.LargeImage = new BitmapImage(new Uri($"pack://application:,,,/{AppVars.AssemblyName};component/Images/{name}", UriKind.RelativeOrAbsolute));
      return button;
    }
    /// <summary>
    /// Use the PushButtonData property to determine when the ribbon button is enabled. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="button"></param>
    /// <returns></returns>
    public static RibbonButton SetAvailabilityClass<T>(this PushButton button) where T : IExternalCommandAvailability, new()
    {
      button.AvailabilityClassName = typeof(T).FullName;
      return button;
    }
    /// <summary>
    /// Sets the ToolTip of the PushButton
    /// </summary>
    /// <param name="button"></param>
    /// <param name="tip"></param>
    /// <returns></returns>
    public static RibbonButton SetToolTip(this RibbonButton button, string tip)
    {
      button.ToolTip = tip;
      return button;
    }
    /// <summary>
    /// Set Button Long description which is the text that flys out when you hover on a button longer
    /// </summary>
    /// <param name="button"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static RibbonButton SetLongDescription(this RibbonButton button, string description)
    {
      button.LongDescription = description;
      return button;
    }
    /// <summary>
    /// Adds a <see cref="PushButton"/> to the <see cref="RibbonPanel"/> specified
    /// </summary>
    /// <typeparam name="TCommand"><see cref="IExternalCommand"/> class</typeparam>
    /// <param name="panel"><see cref="RibbonPanel"/> to add the Button to</param>
    /// <param name="name"><see cref="string"/> for the visible Button Name in Revit</param>
    /// <returns><see cref="PushButton"/></returns>
    public static PushButton AddPushButton<TCommand>(this RibbonPanel panel, string name) where TCommand : IExternalCommand, new()
    {
      Type command = typeof(TCommand);
      PushButtonData pushButtonData = new PushButtonData(command.FullName, name, Assembly.GetAssembly(command).Location, command.FullName);
      return (PushButton)panel.AddItem(pushButtonData);
    }

    #region PullDown
    /// <summary>
    /// 
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="name"></param>
    /// <param name="buttonText"></param>
    /// <returns></returns>
    public static RibbonButton AddPullDownButton(this RibbonPanel panel, string name, string buttonText)
    {
      PulldownButtonData pushButtonData = new PulldownButtonData(name, buttonText);
      return (PulldownButton)panel.AddItem(pushButtonData);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <param name="pullDownButton"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static PushButton AddPushButton<TCommand>(this PulldownButton pullDownButton, string name) where TCommand : IExternalCommand, new()
    {
      Type command = typeof(TCommand);
      PushButtonData pushButtonData = new PushButtonData(command.FullName, name, Assembly.GetAssembly(command).Location, command.FullName);
      return pullDownButton.AddPushButton(pushButtonData);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="button"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static PulldownButton SetLargeImage(this PulldownButton button, string name)
    {
      button.LargeImage = new BitmapImage(new Uri($"pack://application:,,,/{AppVars.AssemblyName};component/Images/{name}", UriKind.RelativeOrAbsolute));
      return button;
    }
    #endregion

    #region SplitButton
    /// <summary>
    /// 
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="name"></param>
    /// <param name="buttonText"></param>
    /// <returns></returns>
    public static RibbonButton AddSplitButton(this RibbonPanel panel, string name, string buttonText)
    {
      SplitButtonData pushButtonData = new SplitButtonData(name, buttonText);
      return (SplitButton)panel.AddItem(pushButtonData);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <param name="splitButton"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static PushButton AddPushButton<TCommand>(this SplitButton splitButton, string name) where TCommand : IExternalCommand, new()
    {
      Type command = typeof(TCommand);
      PushButtonData pushButtonData = new PushButtonData(command.FullName, name, Assembly.GetAssembly(command).Location, command.FullName);
      return splitButton.AddPushButton(pushButtonData);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="button"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static SplitButton SetItemText(this SplitButton button, string text)
    {
      button.ItemText = text;
      return button;
    }
    #endregion
  }
}