using System;
using System.Diagnostics;

/// <summary>
/// This class provides extension methods for loading UI from XML files.
/// </summary>
public static class WinFormsXMLExtensions
{
    public static void LoadUI(this System.Windows.Forms.Control control)
    {
        LoadUI(control, $"{control.GetType().Name}.xml");
    }

    public static void LoadUI(this System.Windows.Forms.Control control, string xmlFilePath)
    {
        Debug.WriteLine($"Loading UI from {xmlFilePath} for control {control.Name}");
    }
}
