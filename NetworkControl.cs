using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace middleware
{
    public interface INetworkControl
    {
        List<NetworkInterface> GetNetworkInterfaces();
    }
    public class NetworkControl : INetworkControl
    {
        public NetworkControl() { }

        public List<NetworkInterface> GetNetworkInterfaces()
        {
            var interfaces = new List<NetworkInterface>();
            foreach (var nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                var ipProperties = nic.GetIPProperties();
                var ipAddress = ipProperties.UnicastAddresses.FirstOrDefault()?.Address.ToString();
                var isUp = nic.OperationalStatus == OperationalStatus.Up;

                var hasInternet = isUp && CanPingViaInterface(nic.Name);

                interfaces.Add(new NetworkInterface
                {
                    Interfacename = nic.Name,
                    MacAddress = nic.GetPhysicalAddress().ToString(),
                    Gateway = ipProperties.GatewayAddresses.FirstOrDefault()?.Address.ToString(),
                    IP = ipAddress,
                    IsUp = isUp,
                    Internet = hasInternet
                });
            }
            return interfaces;
        }
        private bool CanPingViaInterface(string interfaceName)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"ping -I {interfaceName} -c 1 -W 1 8.8.8.8\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                using var process = Process.Start(psi);
                process.WaitForExit(1000); // tối đa 2 giây
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }
        private void RunBashCommand(string cmd)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{cmd}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            Console.WriteLine($"CMD: {cmd}\nOutput: {output}\nError: {error}");
        }
    }

    public class NetworkInterface
    {
        public string Interfacename { get; set; }
        public string MacAddress { get; set; }
        public string IP { get; set; }
        public string Gateway { get; set; }
        public bool IsUp { get; set; }
        public bool Internet { get; set; }
    }
}
