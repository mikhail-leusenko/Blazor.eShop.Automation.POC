using eShop.BDD.Core.WebHost.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace eShop.BDD.Core.WebHost
{
    public class PortHelper : IPortHelper
    {
        private int ExpectedStartPort;
        public PortHelper(int expectedStartPort = 0)
        {
            ExpectedStartPort = expectedStartPort;
        }

        /// <summary>
        /// Obtain the free tcp port after comparing to the expected one.
        /// </summary>
        /// <returns>Integer representation of TCP port to start the Web Application Host. </returns>
        public int GetFreeTcpPort()
        {
            if (this.ExpectedStartPort <= 0)
            {
                this.ExpectedStartPort = 1025;
            }

            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

            IEnumerable<int> tcpConnectionPorts = properties.GetActiveTcpConnections()
                .Where(n => n.LocalEndPoint.Port >= this.ExpectedStartPort)
                .Select(n => n.LocalEndPoint.Port);

            IEnumerable<int> tcpListenerPorts = properties.GetActiveTcpListeners()
                .Where(n => n.Port >= this.ExpectedStartPort)
                .Select(n => n.Port);

            IEnumerable<int> udpListenerPorts = properties.GetActiveUdpListeners()
                .Where(n => n.Port >= this.ExpectedStartPort)
                .Select(n => n.Port);

            int port = Enumerable
                .Range(this.ExpectedStartPort, ushort.MaxValue)
                .Where(i => !tcpConnectionPorts.Contains(i))
                .Where(i => !tcpListenerPorts.Contains(i))
                .FirstOrDefault(i => !udpListenerPorts.Contains(i));

            tcpConnectionPorts.Append(port);
            tcpListenerPorts.Append(port);
            udpListenerPorts.Append(port);

            return port;
        }
    }
}