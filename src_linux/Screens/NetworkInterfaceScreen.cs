using System.Drawing;

namespace srvmon.Screens
{
    public sealed class NetworkInterfaceScreen : Screen
    {
        private readonly string InterfaceName;

        public NetworkInterfaceScreen(string InterfaceName) { this.InterfaceName = InterfaceName; }

        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            this.RenderTitle(Graphics, "NETWORK STATS - " + this.InterfaceName);
            /* old headline e.g. "eth0"
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(Graphics, this.InterfaceName, new System.Drawing.Font(FontFamily.GenericSansSerif, 14), new Rectangle(0, 0, 128, 14), Color.White, flags);
            */

            var stats = StatCollector.NetworkStats[this.InterfaceName];

            // draw graphs
            Helper.RenderGraph(Graphics, 0, 11, 62, 27, stats.ReceiveSpeed);
            Helper.RenderGraph(Graphics, 65, 11, 62, 27, stats.TransmitSpeed);

            // write RX stats
            var font = new Text.DefaultFont7();
            string speed = Helper.BytesToHumanReadableString((ulong)(stats.ReceiveSpeed[0] + 0.5f)) + "/s";
            font.RenderString(Graphics, "S", Color.White, 0, 41);
            font.RenderString(Graphics, speed, Color.White, 7, 41);
            string total = Helper.BytesToHumanReadableString(stats.ReceivedBytes);
            font.RenderString(Graphics, "T", Color.White, 0, 49);
            font.RenderString(Graphics, total, Color.White, 7, 49);
            string dropped = "0% dropped";
            if (stats.ReceivedPackets > 0)
                dropped = string.Format("{0:0.#}% dropped", (float)stats.DroppedPackets / stats.ReceivedPackets);
            font.RenderString(Graphics, dropped, Color.White, 0, 57);

            // write TX stats
            speed = Helper.BytesToHumanReadableString((ulong)(stats.TransmitSpeed[0] + 0.5f)) + "/s";
            font.RenderString(Graphics, "S", Color.White, 65, 41);
            font.RenderString(Graphics, speed, Color.White, 72, 41);
            total = Helper.BytesToHumanReadableString(stats.TransmittedBytes);
            font.RenderString(Graphics, "T", Color.White, 65, 49);
            font.RenderString(Graphics, total, Color.White, 72, 49);
        }
    }
}
