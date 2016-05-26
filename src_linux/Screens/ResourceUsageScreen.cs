using System.Drawing;

namespace srvmon.Screens
{
    public class ResourceUsageScreen:Screen
    {
        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            this.RenderTitle(Graphics, "RESOURCE USAGE");

            Helper.RenderGraph(Graphics, 0, 11, 128, 27, StatCollector.CPU_Usage, 1);
            (new Text.DefaultFont7()).RenderString(Graphics, "CPU", Color.White, 0, 39);
        }
    }
}
