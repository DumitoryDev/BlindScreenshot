using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Gui.Utils
{
    public static class Utils
    {
        public static ImageSource ToImageSource(this System.Drawing.Icon icon) => Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
    }
}
