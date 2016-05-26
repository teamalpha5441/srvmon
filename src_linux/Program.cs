using System;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using srvmon.Screens;

namespace srvmon
{
    public static class Program
    {
        [DllImport("libc", EntryPoint = "geteuid")]
        public static extern int geteuid();

        public static int Main(string[] args)
        {
            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                Console.Error.WriteLine("Not running on Unix");
                return 1;
            }

            if (geteuid() != 0)
            {
                Console.Error.WriteLine("Not running under root");
                return 1;
            }

            // read config
            Config config = new Config();

            // prepare stat collector
            var statCollector = new StatCollector(config.NetworkInterfaces, config.HardDrives);
            // first update so on next update all values are available
            statCollector.Update(0);
            DateTime lastUpdate = DateTime.Now;
            Thread.Sleep(200);

            // prepare screens
            var screens = new List<Screen>(8);
            // screens.Add(new FontTestScreen());
            screens.Add(new SystemdStatusScreen());
            screens.Add(new ResourceUsageScreen());
            for (byte i = 0; i < config.NetworkInterfaces.Length; i++)
                screens.Add(new NetworkInterfaceScreen(config.NetworkInterfaces[i]));
            screens.Add(new HardDriveScreen(config.HardDrives.Keys.ToArray()));

            DateTime lastScreenChange = DateTime.Now;
            byte screenIndex = 0; // start screen index
            Screen currentScreen = screens[screenIndex];
            using (Display display = new Display(config.SerialPort))
                while (true)
                {
                    DateTime now = DateTime.Now;

                    // change screen if time for it
                    if ((now - lastScreenChange).TotalSeconds > config.ScreenChangeDelay)
                    {
                        screenIndex++;
                        if (screenIndex + 1 > screens.Count)
                            screenIndex = 0;
                        currentScreen = screens[screenIndex];
                        lastScreenChange = now;
                    }

                    // update stat collector
                    statCollector.Update((float)(now - lastUpdate).TotalSeconds);
                    lastUpdate = now;

                    using (Bitmap bitmap = Display.CreateBitmap())
                    {
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                            currentScreen.Render(statCollector, graphics);
                        display.WriteBitmap(bitmap);
                    }

                    Thread.Sleep(1000);
                }
        }
    }
}
