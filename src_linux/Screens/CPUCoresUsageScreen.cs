using System.Drawing;
using srvmon.Text;

namespace srvmon.Screens
{
    public class CPUCoresUsageScreen : Screen
    {
        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            var font = DefaultFont7.Instance;
            Renderer.RenderTitle(Graphics, "CPU USAGE", font);

            var cpuUsage = StatCollector.CPU_Usage;
            int yGraph = 19;

            if (cpuUsage.Count > 2) // multi-core (shows up to 5 cores)
            {
                yGraph = 11 + cpuUsage.Count * 8;
                for (byte i = 0; i < cpuUsage.Count && i < 6; i++)
                {
                    byte ypos = (byte)(11 + i * 8);
                    string coreIdentifier = i < 1 ? "T" : i.ToString();
                    Renderer.RenderString(Graphics, coreIdentifier, font, Color.White, 0, ypos);
                    Renderer.RenderProgressBar(Graphics, cpuUsage[i][0], 8, ypos, 120, 7);
                }
            }
            else Renderer.RenderProgressBar(Graphics, cpuUsage[0][0], 0, 11, 128, 7);

            if (yGraph < 52) // if enough space is available (shows only with max. 4 cores)
                Renderer.RenderGraph(Graphics, cpuUsage[0], 0, (byte)yGraph, 128, (byte)(63 - yGraph), 1f);
        }
    }
}
