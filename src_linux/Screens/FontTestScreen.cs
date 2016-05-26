using System.Drawing;
using srvmon.Text;

namespace srvmon.Screens
{
    public sealed class FontTestScreen : Screen
    {
        public override void Render(StatCollector StatCollector, Graphics Graphics)
        {
            var font = DefaultFont7.Instance;
            Renderer.RenderTitle(Graphics, "FONT TEST SCREEN", font);
            Renderer.RenderString(Graphics, "ABCDEFGHIJKLM", font, Color.White, 0, 11);
            Renderer.RenderString(Graphics, "NOPQRSTUVWXYZ", font, Color.White, 0, 19);
            Renderer.RenderString(Graphics, "abcdefghijklm", font, Color.White, 0, 27);
            Renderer.RenderString(Graphics, "nopqrstuvwxyz", font, Color.White, 0, 35);
            Renderer.RenderString(Graphics, "0 123456789 % / . :", font, Color.White, 0, 43);
        }
    }
}
