#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace UnDemolish
{
    [Transaction(TransactionMode.Manual)]
    public class cmdUnDemolish : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document curDoc = uidoc.Document;

            var exitSelection = false;

            while (!exitSelection)
            {
                try
                {
                    // start the transaction
                    using (Transaction t = new Transaction(curDoc))
                    {
                        t.Start("Undemolish");

                        // prompt the user to select an element
                        var curElem = curDoc.GetElement(uidoc.Selection.PickObject
                            (ObjectType.Element, "Select element to undemolish"));

                        // check if the element has a "Phase Demolished" parameter
                        Parameter paramPhaseDemo = curElem.get_Parameter(BuiltInParameter.PHASE_DEMOLISHED);

                        if (paramPhaseDemo != null)
                        {
                            // set the value of "Phase Demolished" to "None"
                            paramPhaseDemo.Set("None");

                            // commit the transaction
                            t.Commit();

                            // alert the user
                            TaskDialog.Show("Success", "The Phase Demolished parameter has been set to None.");
                            return Result.Succeeded;
                        }
                        else
                        {
                            // rollback the transaction
                            t.RollBack();
                        }
                    }
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    // user cancelled the selection
                    exitSelection = true;
                }
            }          

            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
