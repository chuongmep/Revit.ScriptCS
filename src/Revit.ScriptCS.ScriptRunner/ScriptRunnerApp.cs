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
                var files = Directory.GetFiles(filePath);
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFrom(file);
                    }
                }
            }
            catch (Exception  e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
