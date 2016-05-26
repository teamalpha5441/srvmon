using System.Drawing;

namespace srvmon.Screens
{
    public sealed class FontTestScreen : Screen
    {
        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            this.RenderTitle(Graphics, "FONT TEST SCREEN");

            var font = new Text.DefaultFont7();
            font.RenderString(Graphics, "ABCDEFGHIJKLM", Color.White, 0, 11);
            font.RenderString(Graphics, "NOPQRSTUVWXYZ", Color.White, 0, 19);
            font.RenderString(Graphics, "abcdefghijklm", Color.White, 0, 27);
            font.RenderString(Graphics, "nopqrstuvwxyz", Color.White, 0, 35);
            font.RenderString(Graphics, "0 123456789 % / . :", Color.White, 0, 43);
        }
    }
}
