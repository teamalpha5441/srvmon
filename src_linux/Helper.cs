using System.Drawing;

namespace srvmon
{
    public static class Helper
    {
        private const ushort LIMIT_KILOBYTES = 1024;
        private const uint LIMIT_MEGABYTES = LIMIT_KILOBYTES * 1024;
        private const uint LIMIT_GIGABYTES = LIMIT_MEGABYTES * 1024;
        private const ulong LIMIT_TERABYTES = (ulong)LIMIT_GIGABYTES * 1024;

        public static string BytesToHumanReadableString(ulong Bytes)
        {
            if (Bytes < LIMIT_KILOBYTES)
                return Bytes + " B";
            else if (Bytes < LIMIT_MEGABYTES)
                return ((float)Bytes / LIMIT_KILOBYTES).ToString("0.##") + " KB";
            else if (Bytes < LIMIT_GIGABYTES)
                return ((float)Bytes / LIMIT_MEGABYTES).ToString("0.##") + " MB";
            else if (Bytes < LIMIT_TERABYTES)
                return ((float)Bytes / LIMIT_GIGABYTES).ToString("0.##") + " GB";
            else if (Bytes < LIMIT_TERABYTES * 10)
                return ((float)Bytes / LIMIT_TERABYTES).ToString("0.##") + " TB";
            else if (Bytes < LIMIT_TERABYTES * 100)
                return ((float)Bytes / LIMIT_TERABYTES).ToString("0.#") + " TB";
            else return ((float)Bytes / LIMIT_TERABYTES) + " TB";
        }

        public static T[] ShiftArray<T>(T[] Array)
        {
            for (int i = Array.Length - 1; i >= 1; i--)
                Array[i] = Array[i - 1];
            return Array;
        }

        public static void RenderGraph(Graphics Graphics, byte X, byte Y, byte Width, byte Height, float[] Values, float? MaxValue = null)
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
