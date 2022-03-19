
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Scripting.Hosting;
using System;
using Autodesk.AutoCAD.ApplicationServices;

namespace Revit.ScriptCS.ScriptRunner
{
    public class ScriptRunnerHandler
    {
        public string ScriptText { get; internal set; }

        public string ScriptResult { get; private set; }

        public RoslynEditorViewModel RoslynEditorViewModel { get; set; }
        public IProgress<string> Progress { get; set; }

        public void Execute(Document doc)
        {
            var assembliesToRef = new List<Assembly>
            {
                typeof(object).Assembly, //mscorlib
                typeof(Autodesk.AutoCAD.ApplicationServices.Document).Assembly, // Microsoft.CodeAnalysis.Workspaces
            };

            var namespaces = new List<string>
            {
                "Autodesk.AutoCAD.ApplicationServices.Document",
                "System",
                "System.Collections.Generic",
                "System.IO",
                "System.Linq"
            };

            ScriptGlobals globals = new ScriptGlobals(Progress) { doc = doc};

            var options = ScriptOptions.Default.AddReferences(assembliesToRef).WithImports(namespaces);

            try
            {
                object result = CSharpScript.EvaluateAsync<object>(ScriptText, options, globals).Result;
                if ( !(result is null) )
                    RoslynEditorViewModel.Result += CSharpObjectFormatter.Instance.FormatObject(result) + Environment.NewLine;
                RoslynEditorViewModel.IsRunning = System.Windows.Visibility.Collapsed;
            }
            catch ( AggregateException AggEx )
            {
                AggEx.Handle(ex =>
                                {
                                    RoslynEditorViewModel.Result += CSharpObjectFormatter.Instance.FormatObject(ex) + Environment.NewLine;
                                    return true;
                                }
                            );
                RoslynEditorViewModel.IsRunning = System.Windows.Visibility.Collapsed;
            }
            catch ( System.Exception ex )
            {
                RoslynEditorViewModel.Result += CSharpObjectFormatter.Instance.FormatObject(ex) + Environment.NewLine;
                RoslynEditorViewModel.IsRunning = System.Windows.Visibility.Collapsed;
            }
            finally
            {
                RoslynEditorViewModel.IsRunning = System.Windows.Visibility.Collapsed;
            }
        }

        public string GetName()
        {
            return "A Script Runner";
        }


    }
}