using System.Drawing;
using srvmon.Text;

namespace srvmon.Screens
{
    public class ResourceUsageScreen : Screen
    {
        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            var font = DefaultFont7.Instance;
            Renderer.RenderTitle(Graphics, "RESOURCE USAGE", font);
            Renderer.RenderGraph(Graphics, StatCollector.CPU_Usage, 0, 11, 128, 27, 1);
            Renderer.RenderString(Graphics, "CPU", font, Color.White, 0, 40);
        }
    }
}
