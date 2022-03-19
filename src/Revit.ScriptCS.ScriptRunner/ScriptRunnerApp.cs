using RoslynPad.Roslyn;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Autodesk.AutoCAD.Runtime;

namespace Revit.ScriptCS.ScriptRunner
{
    public class ScriptRunnerApp : IExtensionApplication
    {
        // static BitmapSource GetEmbeddedImage(string name)
        // {
        //     try
        //     {
        //         Assembly a = Assembly.GetExecutingAssembly();
        //         using ( var s = a.GetManifestResourceStream(name) )
        //         {
        //             return BitmapFrame.Create(s);
        //         }
        //     }
        //     catch
        //     {
        //         return null;
        //     }
        // }


        public void Initialize()
        {
            FrmSharpShell command = new FrmSharpShell();
            command.ShowDialog();
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
