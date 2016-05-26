using System.Drawing;
using srvmon.Text;

namespace srvmon.Screens
{
    public sealed class NetworkInterfaceScreen : Screen
    {
        private readonly string InterfaceName;

        public NetworkInterfaceScreen(string InterfaceName) { this.InterfaceName = InterfaceName; }

        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            var font = DefaultFont7.Instance;
            Renderer.RenderTitle(Graphics, "NETWORK STATS - " + this.InterfaceName, font);

            var stats = StatCollector.NetworkStats[this.InterfaceName];

            // draw graphs
            Renderer.RenderGraph(Graphics, stats.ReceiveSpeed, 0, 11, 62, 27);
            Renderer.RenderGraph(Graphics, stats.TransmitSpeed, 65, 11, 62, 27);

            // write RX stats
            string speed = Helper.BytesToHumanReadableString((ulong)(stats.ReceiveSpeed[0] + 0.5f)) + "/s";
            Renderer.RenderString(Graphics, "S", font, Color.White, 0, 41);
            Renderer.RenderString(Graphics, speed, font, Color.White, 7, 41);
            string total = Helper.BytesToHumanReadableString(stats.ReceivedBytes);
            Renderer.RenderString(Graphics, "T", font, Color.White, 0, 49);
            Renderer.RenderString(Graphics, total, font, Color.White, 7, 49);
            string dropped = "0% dropped";
            if (stats.ReceivedPackets > 0)
                dropped = string.Format("{0:0.#}% dropped", (float)stats.DroppedPackets / stats.ReceivedPackets);
            Renderer.RenderString(Graphics, dropped, font, Color.White, 0, 57);

            // write TX stats
            speed = Helper.BytesToHumanReadableString((ulong)(stats.TransmitSpeed[0] + 0.5f)) + "/s";
            Renderer.RenderString(Graphics, "S", font, Color.White, 65, 41);
            Renderer.RenderString(Graphics, speed, font, Color.White, 72, 41);
            total = Helper.BytesToHumanReadableString(stats.TransmittedBytes);
            Renderer.RenderString(Graphics, "T", font, Color.White, 65, 49);
            Renderer.RenderString(Graphics, total, font, Color.White, 72, 49);
        }
    }
}
