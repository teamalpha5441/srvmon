using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace srvmon
{
    public sealed class StatCollector
    {
        private const ushort ARRAY_BUFFER_LENGTH = 128;

        public struct NetworkInterfaceStats
        {
            public ulong ReceivedBytes, ReceivedPackets, DroppedPackets;
            public float[] ReceiveSpeed;
            public ulong TransmittedBytes, TransmittedPackets;
            public float[] TransmitSpeed;
        }

        public enum SysStatus
        {
            Running,
            Degraded,
            Unknown
        }

        public enum HDDStatus
        {
            Active,
            Standby,
            Offline
        }

        private bool FirstUpdate;
        private string[] NetworkInterfaces;
        private ulong[] CPU_TotalJiffies;
        private ulong[] CPU_WorkJiffies;
        private ulong Mem_Total;
        private Dictionary<string, string> HardDrives;
        private readonly byte CpuCount;
        private float RareUpdateElapsedSeconds;

        public Dictionary<string, NetworkInterfaceStats> NetworkStats { get; private set; }
        public DateTime BootTime { get; private set; }
        public List<float[]> CPU_Usage { get; private set; }
        public float[] RAM_Usage { get; private set; }
        public SysStatus SystemdStatus { get; private set; }
        public List<string> SystemdFailedUnits { get; private set; }
        public Dictionary<string, HDDStatus> HardDriveStatus { get; private set; }

        public StatCollector(string[] NetworkInterfaces, Dictionary<string, string> HardDrives)
        {
            // prepare private fields
            this.FirstUpdate = true;
            this.NetworkInterfaces = NetworkInterfaces;
            foreach (string line in File.ReadAllLines("/proc/meminfo"))
                if (line.StartsWith("MemTotal:"))
                {
                    int indexOfColon = line.IndexOf(':');
                    this.Mem_Total = 1024 * Convert.ToUInt64(line.Substring(indexOfColon + 1, line.Length - indexOfColon - 3)); // remove "kB" suffix!
                    break;
                }
            if (this.Mem_Total < 1)
                throw new IOException("Couldn't read total memory amount");
            this.HardDrives = HardDrives;
            this.CpuCount = (byte)(Environment.ProcessorCount + 1);
            this.RareUpdateElapsedSeconds = 0;

            // prepare public fields
            this.NetworkStats = new Dictionary<string, NetworkInterfaceStats>(NetworkInterfaces.Length);
            bool bootTimeSet = false;
            foreach (string line in File.ReadLines("/proc/stat"))
            {
                int indexOfSpace = line.IndexOf(' ');
                if (line.Substring(0, indexOfSpace) == "btime")
                {
                    this.BootTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    this.BootTime = this.BootTime.AddSeconds(Convert.ToUInt32(line.Substring(indexOfSpace + 1))).ToLocalTime();
                    bootTimeSet = true;
                    break;
                }
            }
            if (!bootTimeSet)
                throw new IOException("Couldn't read boot time");
            this.CPU_Usage = new List<float[]>(this.CpuCount);
            for (byte i = 0; i < this.CpuCount; i++)
                this.CPU_Usage.Add(new float[ARRAY_BUFFER_LENGTH]);
            this.RAM_Usage = new float[ARRAY_BUFFER_LENGTH];
            this.SystemdFailedUnits = new List<string>();
            this.HardDriveStatus = new Dictionary<string, HDDStatus>(HardDrives.Count);
        }

        public void Update(float ElapsedSeconds)
        {
            if (this.NetworkInterfaces.Length > 0)
            {
                string[] lines = File.ReadAllLines("/proc/net/dev");
                for (int i = 2; i < lines.Length; i++)
                {
                    string line = lines[i];
                    int indexOfColon = line.IndexOf(':');
                    string ifName = line.Substring(0, indexOfColon).Trim();
                    if (this.NetworkInterfaces.Contains(ifName))
                    {
                        string[] fields = line.Substring(indexOfColon + 1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        NetworkInterfaceStats newStats = new NetworkInterfaceStats();
                        newStats.ReceivedBytes = Convert.ToUInt64(fields[0]);
                        newStats.ReceivedPackets = Convert.ToUInt64(fields[1]);
                        newStats.DroppedPackets = Convert.ToUInt64(fields[3]);
                        newStats.TransmittedBytes = Convert.ToUInt64(fields[8]);
                        newStats.TransmittedPackets = Convert.ToUInt64(fields[9]);
                        NetworkInterfaceStats oldStats;
                        if (!this.FirstUpdate && this.NetworkStats.TryGetValue(ifName, out oldStats))
                        {
                            newStats.ReceiveSpeed = Helper.ShiftArray<float>(oldStats.ReceiveSpeed);
                            newStats.ReceiveSpeed[0] = (newStats.ReceivedBytes - oldStats.ReceivedBytes) / ElapsedSeconds;
                            newStats.TransmitSpeed = Helper.ShiftArray<float>(oldStats.TransmitSpeed);
                            newStats.TransmitSpeed[0] = (newStats.TransmittedBytes - oldStats.TransmittedBytes) / ElapsedSeconds;
                            this.NetworkStats[ifName] = newStats;
                        }
                        else
                        {
                            newStats.ReceiveSpeed = new float[ARRAY_BUFFER_LENGTH];
                            newStats.TransmitSpeed = new float[ARRAY_BUFFER_LENGTH];
                            this.NetworkStats.Add(ifName, newStats);
                        }
                    }
                }
            }

            ulong[] CPU_TotalJiffies = new ulong[this.CpuCount];
            ulong[] CPU_WorkJiffies = new ulong[this.CpuCount];
            {
                string[] lines = File.ReadAllLines("/proc/stat");
                for (byte i = 0; i < this.CpuCount; i++)
                {
                    int indexOfSpace = lines[i].IndexOf(' ');
                    string[] fields = lines[i].Substring(indexOfSpace + 1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    // see https://github.com/hishamhm/htop/blob/e0209da88faf3b390d71ff174065abd407abfdfd/ProcessList.c#L942

                    ulong usertime = Convert.ToUInt64(fields[0]);
                    ulong nicetime = Convert.ToUInt64(fields[1]);
                    ulong systemtime = Convert.ToUInt64(fields[2]);
                    ulong idletime = Convert.ToUInt64(fields[3]);
                    ulong ioWait = Convert.ToUInt64(fields[4]);
                    ulong irq = Convert.ToUInt64(fields[5]);
                    ulong softIrq = Convert.ToUInt64(fields[6]);
                    ulong steal = Convert.ToUInt64(fields[7]);
                    ulong guest = Convert.ToUInt64(fields[8]);
                    ulong guestnice = Convert.ToUInt64(fields[9]);

                    usertime = usertime - guest;
                    nicetime = nicetime - guestnice;

                    ulong idlealltime = idletime + ioWait;
                    ulong systemalltime = systemtime + irq + softIrq;
                    ulong virtalltime = guest + guestnice;

                    CPU_WorkJiffies[i] = usertime + nicetime + systemalltime + virtalltime;
                    CPU_TotalJiffies[i] = CPU_WorkJiffies[i] + idlealltime; // + steal
                }
            }
            if (!this.FirstUpdate)
                for (byte i = 0; i < this.CpuCount; i++)
                {
                    this.CPU_Usage[i] = Helper.ShiftArray<float>(this.CPU_Usage[i]);
                    this.CPU_Usage[i][0] = (float)(CPU_WorkJiffies[i] - this.CPU_WorkJiffies[i]) / (CPU_TotalJiffies[i] - this.CPU_TotalJiffies[i]);
                }
            this.CPU_TotalJiffies = CPU_TotalJiffies;
            this.CPU_WorkJiffies = CPU_WorkJiffies;

            ulong memAvailable = 0;
            foreach (string line in File.ReadAllLines("/proc/meminfo"))
                if (line.StartsWith("MemAvailable:"))
                {
                    int indexOfColon = line.IndexOf(':');
                    memAvailable = 1024 * Convert.ToUInt64(line.Substring(indexOfColon + 1, line.Length - indexOfColon - 3)); // remove "kB" suffix!
                }
            this.RAM_Usage = Helper.ShiftArray<float>(this.RAM_Usage);
            this.RAM_Usage[0] = 1 - (float)memAvailable / this.Mem_Total;

            this.RareUpdateElapsedSeconds += ElapsedSeconds;
            if (this.FirstUpdate || this.RareUpdateElapsedSeconds > 5)
            {
                RareUpdate();
                this.RareUpdateElapsedSeconds = 0;
            }

            this.FirstUpdate = false;
        }

        private void RareUpdate()
        {
            using (var systemctlProcess = Process.Start(new ProcessStartInfo("/usr/bin/systemctl", "is-system-running")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }))
            {
                this.SystemdFailedUnits.Clear();
                switch (systemctlProcess.StandardOutput.ReadToEnd().Trim())
                {
                    case "running": this.SystemdStatus = SysStatus.Running; break;
                    case "degraded": this.SystemdStatus = SysStatus.Degraded; break;
                    default: this.SystemdStatus = SysStatus.Unknown; break;
                }
            }
            this.SystemdFailedUnits.Clear();
            if (this.SystemdStatus != SysStatus.Running)
                using (var systemctlProcess = Process.Start(new ProcessStartInfo("/usr/bin/systemctl", "list-units --state=failed --no-legend -l")
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true
                    }))
                {
                    while (!systemctlProcess.StandardOutput.EndOfStream)
                    {
                        string line = systemctlProcess.StandardOutput.ReadLine();
                        int indexOfSpace = line.IndexOf(' ');
                        if (indexOfSpace >= 0)
                            this.SystemdFailedUnits.Add(line.Substring(0, indexOfSpace));
                        else this.SystemdFailedUnits.Add(line);
                    }
                }

            this.HardDriveStatus.Clear();
            foreach (var hardDrive in this.HardDrives)
            {
                var status = HDDStatus.Offline;
                if (File.Exists(hardDrive.Value))
                    using (var hdparmProcess = Process.Start(new ProcessStartInfo("/usr/bin/hdparm", "-C " + hardDrive.Value)
                        {
                            UseShellExecute = false,
                            RedirectStandardOutput = true
                        }))
                        while (!hdparmProcess.StandardOutput.EndOfStream)
                        {
                            string line = hdparmProcess.StandardOutput.ReadLine();
                            if (line.Contains("drive state is:"))
                            {
                                switch (line.Substring(line.IndexOf(':') + 1).Trim())
                                {
                                    case "active/idle": status = HDDStatus.Active; break;
                                    case "standby": status = HDDStatus.Standby; break;
                                }
                                break;
                            }
                        }
                this.HardDriveStatus.Add(hardDrive.Key, status);
            }
        }
    }
}
