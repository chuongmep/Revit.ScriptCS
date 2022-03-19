using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Revit.ScriptCS.ScriptRunner
{
    public static class ImageUtils
    {
        /// <summary>
        /// Return Bitmap image has embed in resources
        /// </summary>
        /// <param name="name"></param>
        /// <returns name="bitmapimage">bitmapimage</returns>
        static BitmapSource GetEmbeddedImage( string name)
        {
            try
            {
                Assembly a = Assembly.GetExecutingAssembly();
                using (var s = a.GetManifestResourceStream(name))
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
