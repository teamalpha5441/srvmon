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

            //TODO draw current date and time
            //TODO draw server uptime
            //TODO logged in users?

            // draw status string
            //TODO bigger font
            string status = Enum.GetName(typeof(StatCollector.SystemStatus), StatCollector.SystemdStatus).ToLower();
            float xpos = 64.5f - font.MeasureString(status) / 2f;
            Renderer.RenderString(Graphics, status, font, Color.White, (int)xpos, 26);
            
            // draw failed service names
            if (StatCollector.SystemdFailedUnits.Count > 0)
            {
                StatCollector.SystemdFailedUnits.Sort();
                for (byte i = 0; i < 2 && i < StatCollector.SystemdFailedUnits.Count; i++)
                    Renderer.RenderString(Graphics, StatCollector.SystemdFailedUnits[i], font, Color.White, 0, 49 + i * 8);
            }
        }
    }
}
