using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace EstomedApp
{
    class NetUtil
    {
        private int minPort;
        private int maxPort;
        private ScanPortsCb cb;
        private string host;

        public interface ScanPortsCb
        {
            void onScanProgress(double pr);
            void onScanResult(List<int> usedPort);
        }

        public NetUtil(ScanPortsCb _cb, string _host = "localhost", int _minPort = 22, int _maxPort = 64000) {
            cb = _cb;
            host = _host;
            minPort = _minPort;
            maxPort = _maxPort;
        }

        public static bool IsLocalHost(string host)
        {
            IPAddress[] hosts;
            IPAddress[] locals;
            hosts = Dns.GetHostAddresses(host);
            locals = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress hostAddress in hosts)
            {
                if (IPAddress.IsLoopback(hostAddress))
                    return true;
                else
                {
                    foreach (IPAddress localAddress in locals)
                    {
                        if (hostAddress.Equals(localAddress))
                            return true;
                    }
                }
            }
            return false;
        }

        public void ScanPorts()
        {
            List<int> usedPort = new List<int>();
            if (IsLocalHost(host))
            {
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
                int i = 0;
                foreach (IPEndPoint endpoint in tcpConnInfoArray)
                {
                    if(!usedPort.Contains(endpoint.Port))
                        usedPort.Add(endpoint.Port);
                    cb.onScanProgress(100 * i / tcpConnInfoArray.Length);
                }
            } else
            {
                cb.onScanProgress(0);
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "netstat";
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                string pattern = host + ":(\\d+)";
                MessageBox.Show(pattern + " " + output);
                Regex rgx = new Regex(pattern);
                foreach (Match match in rgx.Matches(output))
                {
                    usedPort.Add(Int32.Parse(match.Groups[1].Value));
                }
            }
            cb.onScanResult(usedPort);
        }
    }
}
