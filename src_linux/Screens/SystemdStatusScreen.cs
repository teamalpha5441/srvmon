using System;
using System.Drawing;

namespace srvmon.Screens
{
    public sealed class SystemdStatusScreen : Screen
    {
        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            this.RenderTitle(Graphics, "SYSTEMD STATUS");

            var font = new Text.DefaultFont7();

            // draw status string
            //TODO bigger font
            string status = Enum.GetName(typeof(StatCollector.SystemStatus), StatCollector.SystemdStatus).ToLower();
            float xpos = 64.5f - font.MeasureString(status) / 2f;
            font.RenderString(Graphics, status, Color.White, (int)xpos, 26);
            
            // draw failed service names
            if (StatCollector.SystemdFailedUnits.Count > 0)
            {
                StatCollector.SystemdFailedUnits.Sort();
                for (byte i = 0; i < 2 && i < StatCollector.SystemdFailedUnits.Count; i++)
                    (new Text.DefaultFont7()).RenderString(Graphics, StatCollector.SystemdFailedUnits[i], Color.White, 0, 49 + i * 8);
            }
        }
    }
}
