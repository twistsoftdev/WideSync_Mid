using System.Diagnostics;

namespace middleware
{
    public interface INetworkControl
    {
        List<string> GetNetworkInterfaces();
    }
    public class NetworkControl : INetworkControl
    {
        public NetworkControl() { }

        public List<string> GetNetworkInterfaces()
        {
            var interfaces = new List<string>();
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "bash",
                    Arguments = "-c \"ip link show | grep -E '^[0-9]+:' | awk '{print $2}' | sed 's/://'\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                var line = process.StandardOutput.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    interfaces.Add(line);
                }
            }
            process.WaitForExit();

            return interfaces;
        }
    }
}
