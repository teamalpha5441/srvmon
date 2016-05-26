using System.Drawing;
using srvmon.Text;

namespace srvmon
{
    public static class Renderer
    {
        public static void RenderTitle(Graphics Graphics, string Title, Text.Font Font)
        {
            RenderString(Graphics, Title, Font, Color.White, 0, 0);
            Graphics.DrawLine(Pens.White, 0, 8, 127, 8);
        }

        public static void RenderString(Graphics Graphics, string String, Text.Font Font, Color Color, int X, int Y)
        {
            if (String.Length > 0)
                using (Bitmap b = new Bitmap(Font.MeasureString(String), Font.MaxHeight))
                {
                    int xpos = 0;
                    for (ushort i = 0; i < String.Length; i++)
                    {
                        Letter letter = Font.GetLetter(String[i]);
                        int ypos = Font.MaxHeight - letter.Height;

                        for (byte y = 0; y < letter.Height; y++)
                            for (byte x = 0; x < letter.Width; x++)
                                if (letter.Data[y * letter.Width + x])
                                    b.SetPixel(xpos + x, ypos + y, Color);

                        xpos += letter.Width + 1;
                    }
                    Graphics.DrawImage(b, X, Y);
                }
        }

        public static void RenderGraph(Graphics Graphics, float[] Values, byte X, byte Y, byte Width, byte Height, float? MaxValue = null)
        {
            if (!MaxValue.HasValue)
            {
                MaxValue = Values[0];
                for (int i = 1; i < Values.Length; i++)
                    if (Values[i] > MaxValue.Value)
                        MaxValue = Values[i];
            }

            Graphics.DrawLine(Pens.White, X, Y, X, Y + Height);
            Graphics.DrawLine(Pens.White, X, Y + Height, X + Width, Y + Height);

            PointF? lastPoint = null;
            for (int i = 0; true; i++)
            {
                int xpos = X + Width - i;
                if (xpos <= X)
                    break;
                float ypos = Y + (Height * (1 - Values[i] / MaxValue.Value));
                PointF currentPoint = new PointF(xpos, ypos);
                if (lastPoint.HasValue)
                    Graphics.DrawLine(Pens.White, lastPoint.Value, currentPoint);
                lastPoint = currentPoint;
            }
        }
    }
}
