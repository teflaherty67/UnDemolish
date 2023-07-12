#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Windows.Markup;

#endregion

namespace UnDemolish
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            // Create ribbon panel 
            RibbonPanel panel = app.CreateRibbonPanel("Modify");

            // Create button data instances
            ButtonDataClass btnUndemolish = new ButtonDataClass("btnUnDemolish", "Undemolish", cmdUnDemolish.GetMethod(),
                Properties.Resources.Undemolish_32, Properties.Resources.Undemolish_16,
                "Sets the Phase Demolioshed paramter to None for the selected item");

            // Create buttons
            PushButton btnData1 = panel.AddItem(btnUndemolish.Data) as PushButton;
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
