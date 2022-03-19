using RoslynPad.Roslyn;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Autodesk.AutoCAD.Runtime;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;
using Exception = System.Exception;

namespace Revit.ScriptCS.ScriptRunner
{
    public class ScriptRunnerApp : IExtensionApplication
    {

        public void Initialize()
        {
            ResolveAssembly();
            //ScriptRunnerExternalCommand command = new ScriptRunnerExternalCommand();
            //command.ShowFormWPF();
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Sharp Shell Loaded...");

        }

        public void Terminate()
        {
            //Nothing
        }

        public void ResolveAssembly()
        {
            try
            {
                string filePath = Assembly.GetExecutingAssembly().Location;
                string directoryName = Path.GetDirectoryName(filePath);
                var files = Directory.GetFiles(directoryName);
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        WriteConsole($"Loading....{file}");
                        Assembly.LoadFrom(file);
                    }
                }
            }
            catch (Exception  e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        void WriteConsole(string message)
        {
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(message);
        }
    }
}
