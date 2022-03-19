using System;
using Autodesk.AutoCAD.ApplicationServices;

namespace Revit.ScriptCS.ScriptRunner
{
    public class ScriptGlobals
    {
        public Document doc;
        private readonly IProgress<string> progress;

        public ScriptGlobals(IProgress<string> Progress)
        {
            progress = Progress;
        }

        public void Print(string Message)
        {
            progress.Report(Message);
        }
    }
}