using System.Drawing;

namespace srvmon.Screens
{
    public sealed class HardDriveScreen : Screen
    {
        private const char DOTTED_CIRCLE = '\u25CC';
        private const char EMPTY_CIRCLE = '\u25CB';
        private const char FILLED_CIRCLE = '\u25CF';

        private string[] HardDrives;

        public HardDriveScreen(string[] HardDrives) { this.HardDrives = HardDrives;}

        private char GetCharFromStatus(StatCollector.HDDStatus Status)
        {
            if (Status == StatCollector.HDDStatus.Active)
                return FILLED_CIRCLE;
            else if (Status == StatCollector.HDDStatus.Standby)
                return EMPTY_CIRCLE;
            else return DOTTED_CIRCLE;
        }

        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            this.RenderTitle(Graphics, "HARD DRIVE STATUS");

            var font = new Text.DefaultFont7();
            for (byte i = 0; i < 10 && i < HardDrives.Length; i++)
            {
                var status = StatCollector.HardDriveStatus[HardDrives[i]];
                font.RenderString(Graphics, GetCharFromStatus(status) + " " + HardDrives[i], Color.White, i > 4 ? 66 : 2, 12 + i * 10 - (i > 4 ? 50 : 0));
            }
        }
    }
}
