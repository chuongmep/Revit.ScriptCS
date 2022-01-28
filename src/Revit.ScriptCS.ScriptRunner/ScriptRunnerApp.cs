using Autodesk.Revit.UI;
using RoslynPad.Roslyn;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Revit.ScriptCS.ScriptRunner
{
    public class ScriptRunnerApp : IExternalApplication
    {
        internal static ScriptRunnerApp thisApp = null;
        static readonly string ExecutingAssemblyPath = Assembly.GetExecutingAssembly().Location;
        private Window scriptEditor;

        //private Thread scriptEditorThread;
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            scriptEditor = null;
            thisApp = this;

            RibbonPanel rvtRibbonPanel = application.CreateRibbonPanel("Scripting");

            PushButtonData pushButtonData = new PushButtonData("RunScriptCS", "C# Scripting", ExecutingAssemblyPath, "Revit.ScriptCS.ScriptRunner.ScriptRunnerExternalCommand");
            PushButton runButton = rvtRibbonPanel.AddItem(pushButtonData) as PushButton;

            runButton.ToolTip = "Run C# Scripts";
            runButton.Image = GetEmbeddedImage("Revit.ScriptCS.ScriptRunner.Resources.logo_Csharp_16x16.png");
            runButton.LargeImage = GetEmbeddedImage("Revit.ScriptCS.ScriptRunner.Resources.logo_Csharp_32x32.png");

            return Result.Succeeded;
        }

        static BitmapSource GetEmbeddedImage(string name)
        {
            try
            {
                Assembly a = Assembly.GetExecutingAssembly();
                using ( var s = a.GetManifestResourceStream(name) )
                {
                    return BitmapFrame.Create(s);
                }
            }
            catch
            {
                return null;
            }
        }

        

    }
}
