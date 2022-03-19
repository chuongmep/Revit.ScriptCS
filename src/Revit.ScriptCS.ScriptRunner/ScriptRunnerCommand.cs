using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Windows;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using RoslynPad.Roslyn;
using Exception = System.Exception;

namespace Revit.ScriptCS.ScriptRunner
{

    public class ScriptRunnerExternalCommand
    {

        [CommandMethod("SharpShellConsole")]
        public void Execute()
        {
            try
            {
                ShowFormWPF();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void ShowFormWPF()
        {

            var assembliesToRef = new List<Assembly>
                {
                    typeof(object).Assembly, //mscorliby,
                    typeof(Autodesk.AutoCAD.ApplicationServices.Document).Assembly,
                    Assembly.Load("RoslynPad.Roslyn.Windows"),
                    Assembly.Load("RoslynPad.Editor.Windows"),
                };

            // var namespaces = new List<string>
            // {
            //     "Autodesk.Revit.UI",
            //     "Autodesk.Revit.DB"
            //     "Autodesk.Revit.DB.Structure",
            //     "System",
            //     "System.Collections.Generic",
            //     "System.IO",
            //     "System.Linq"
            // };

            var roslynHost = new RevitRoslynHost(
                additionalAssemblies: assembliesToRef,
                references: RoslynHostReferences.NamespaceDefault.With(typeNamespaceImports: new[]
                {
                        typeof(Document), typeof(Dictionary<,>),
                        typeof(System.Linq.Enumerable), typeof(ScriptGlobals)
                }),
                disabledDiagnostics: ImmutableArray.Create("CS1701", "CS1702", "CS0518"));
            var document = new RoslynEditorViewModel(roslynHost);
            RoslynEditor scriptEditor = new RoslynEditor(document);
            scriptEditor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            // Show result doc of roslynHost
            // handler.Progress =
            //     new Progress<string>(message => document.Result += message + Environment.NewLine);
            scriptEditor.ShowDialog();
        }
    }
}