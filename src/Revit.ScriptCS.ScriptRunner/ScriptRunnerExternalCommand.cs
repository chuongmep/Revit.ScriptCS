using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using RoslynPad.Roslyn;

namespace Revit.ScriptCS.ScriptRunner
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ScriptRunnerExternalCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
			try
            {
                ShowWPF();
				return Result.Succeeded;
			}
			catch ( Exception ex)
			{
                message = ex.Message;
                return Result.Failed;
            }
        }

        public void ShowWPF()
        {
            try
            {
                
                var handler = new ScriptRunnerHandler();
                ExternalEvent externalEvent = ExternalEvent.Create(handler);
                var assembliesToRef = new List<Assembly>
                    {
                        typeof(object).Assembly, //mscorlib
                        typeof(Autodesk.Revit.UI.UIApplication).Assembly,
                        typeof(Autodesk.Revit.DB.Document).Assembly,
                        Assembly.Load("RoslynPad.Roslyn.Windows"),
                        Assembly.Load("RoslynPad.Editor.Windows")
                    };
                var roslynHost = new RevitRoslynHost(
                    additionalAssemblies: assembliesToRef,
                    references: RoslynHostReferences.NamespaceDefault.With(typeNamespaceImports: new[] { typeof(UIApplication), typeof(Autodesk.Revit.DB.Document), typeof(Dictionary<,>), typeof(System.Linq.Enumerable), typeof(ScriptGlobals) }),
                    disabledDiagnostics: ImmutableArray.Create("CS1701", "CS1702", "CS0518"));

                var document = new RoslynEditorViewModel(roslynHost, externalEvent, handler);
                RoslynEditor scriptEditor = new RoslynEditor(document);
                scriptEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                handler.Progress = new Progress<string>(message => document.Result += message + Environment.NewLine);
                scriptEditor.Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //throw;
            }
        }
    }
}
