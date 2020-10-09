using System.Diagnostics;
using System.Windows.Media;

namespace Gui.View
{
    public class ProcessView
    {
        public Process Process { get; set; }
        public ImageSource Image { get; set; }
        public string DisplayName { get; set; }

        public ProcessView(Process proc, ImageSource source)
        {
            this.Process = proc;
            this.Image = source;
            this.DisplayName = proc.ProcessName + " " + proc.Id;
        }

        public override string ToString()
        {
            return this.DisplayName.ToLower();
        }
    }
}
