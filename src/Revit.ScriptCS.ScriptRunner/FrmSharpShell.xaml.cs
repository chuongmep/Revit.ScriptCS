using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Revit.ScriptCS.ScriptRunner
{
    /// <summary>
    /// Interaction logic for FrmSharpShell.xaml
    /// </summary>
    public partial class FrmSharpShell : Window
    {
        public FrmSharpShell()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           ScriptRunnerExternalCommand command=new ScriptRunnerExternalCommand();
           command.ShowFormWPF();
        }
    }
}
