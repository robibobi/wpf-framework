using System.ComponentModel;
using System.Windows;

namespace Tcoc.Wpf.Framework.Util
{
    public static class DesignTimeUtils
    {
        private static readonly DependencyObject doInstance
            = new DependencyObject();

        public static bool IsRunningInDesigner 
            => DesignerProperties.GetIsInDesignMode(doInstance);
    }
}
