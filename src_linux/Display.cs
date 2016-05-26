using System;
using System.Drawing;
using System.IO.Ports;
using System.Drawing.Imaging;

namespace srvmon
{
    public class Display : SerialPort
    {
        public Display(string DeviceFile)
            : base(DeviceFile, 921600)
        // https://bugzilla.xamarin.com/show_bug.cgi?id=8207
        { base.Open(); }

        public static Bitmap CreateBitmap() { return new Bitmap(128, 64, PixelFormat.Format32bppArgb); }

        public void WriteBitmap(Bitmap Bitmap)
        {
            if (Bitmap.Width != 128 && Bitmap.Height != 64)
                throw new ArgumentException("Bitmap size must be 128x64");

            byte[] buffer = new byte[16 * 64];
            BitmapData bmd = null;
            try
            {
                bmd = Bitmap.LockBits(new Rectangle(0, 0, 128, 64), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                unsafe
                {
                    byte* scan0 = (byte*)bmd.Scan0;
                    for (ushort i = 0; i < buffer.Length; i++)
                        for (byte b = 0; b < 8; b++)
                            if (scan0[i * 32 + b * 4 + 3] > 100)
                                buffer[i] |= (byte)(1 << b);
                }
            }
            finally { Bitmap.UnlockBits(bmd); }

            base.Write(buffer, 0, buffer.Length);
        }
    }
}
