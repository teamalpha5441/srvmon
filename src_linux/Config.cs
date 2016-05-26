using System.Collections.Generic;

namespace srvmon
{
    public sealed class Config
    {
        public readonly string SerialPort = "/dev/ttyACM0";

        public readonly ushort ScreenChangeDelay = 5; // seconds

        public readonly string[] NetworkInterfaces = new string[1] { "eth0" };

        public readonly Dictionary<string, string> HardDrives = new Dictionary<string, string>() { { "ROOT", "/dev/sda1" } };
    }
}
