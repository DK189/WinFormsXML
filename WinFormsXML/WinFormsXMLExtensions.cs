using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

/// <summary>
/// This class provides extension methods for loading UI from XML files.
/// </summary>
public static partial class WinFormsXMLExtensions
{

}

partial class WinFormsXMLExtensions
{
    static readonly ConditionalWeakTable<System.Windows.Forms.Control, object> ControlModels = new ConditionalWeakTable<System.Windows.Forms.Control, object>();

    /// <summary>
    /// Sets the model for the control.
    /// </summary>
    /// <param name="control"></param>
    /// <param name="model"></param>
    public static void Model(this System.Windows.Forms.Control control, object model)
    {
        ControlModels.Remove(control);
        ControlModels.Add(control, model);
    }

    /// <summary>
    /// Gets the model for the control.
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static object Model(this System.Windows.Forms.Control control)
    {
        return ControlModels.TryGetValue(control, out var model) ? model : null;
    }


}
