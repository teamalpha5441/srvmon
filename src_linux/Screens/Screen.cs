using System.Drawing;

namespace srvmon.Screens
{
    public abstract class Screen
    {
        public abstract void Render(StatCollector StatCollector, Graphics Graphics);

        public void RenderTitle(Graphics Graphics, string Title)
        {
            (new Text.DefaultFont7()).RenderString(Graphics, Title, Color.White, 0, 0);
            Graphics.DrawLine(Pens.White, 0, 8, 127, 8);
        }
    }
}
