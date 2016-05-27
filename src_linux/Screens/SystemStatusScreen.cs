using System;
using System.Drawing;
using srvmon.Text;

namespace srvmon.Screens
{
    public sealed class SystemStatusScreen : Screen
    {
        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            var font = DefaultFont7.Instance;
            Renderer.RenderTitle(Graphics, "SYSTEM STATUS", font);

            // write date and time
            DateTime now = DateTime.Now;
            Renderer.RenderString(Graphics, now.ToString("yyyy-MM-dd HH:mm:ss"), font, Color.White, 0, 11);

            // write unix time
            double unixTime = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            Renderer.RenderString(Graphics, "Unix Time: " + (uint)unixTime, font, Color.White, 0, 19);

            // write uptime
            string uptime = (now - StatCollector.BootTime).ToString(@"d\d\ hh\:mm\:ss");
            Renderer.RenderString(Graphics, "Uptime: " + uptime, font, Color.White, 0, 27);

            // Y = 35

            // write systemd status
            string systemdStatus = Enum.GetName(typeof(StatCollector.SysStatus), StatCollector.SystemdStatus);
            Renderer.RenderString(Graphics, "Systemd Status: " + systemdStatus, font, Color.White, 0, 49);

            // write first failed unit
            if (StatCollector.SystemdFailedUnits.Count > 0)
            {
                StatCollector.SystemdFailedUnits.Sort();
                Renderer.RenderString(Graphics, StatCollector.SystemdFailedUnits[0], font, Color.White, 0, 57);
            }
        }
    }
}
