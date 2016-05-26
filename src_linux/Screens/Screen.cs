using System.Drawing;

namespace srvmon.Screens
{
    public abstract class Screen
    {
        public abstract void Render(StatCollector StatCollector, Graphics Graphics);
    }
}
