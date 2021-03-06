﻿using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace STA.Utils
{
    class FeaturesUtils : BaseUtils
    {
        /// <summary>
        /// Metodo para obtener la MACADDR del cliente
        /// </summary>
        /// <returns>Direccion MAC del PC</returns>
        public PhysicalAddress getMacAddress()
        {
            try
            {
                foreach (NetworkInterface nicInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Only consider Ethernet network interfaces
                    if (nicInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nicInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        return nicInterface.GetPhysicalAddress();
                    }
                }
            }
            catch (Exception e)
            {

                BaseUtils.log.Error(e);
            }

            return null;
        }

        /// <summary>
        /// Metodo para obtener la IPADDR interna del cliente
        /// </summary>
        /// <returns>Direccion IP Local</returns>
        public string getLocalIPAddress()
        {
            var hostName = Dns.GetHostEntry(Dns.GetHostName());

            try
            {
                foreach (var ipAddr in hostName.AddressList)
                {
                    if (ipAddr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ipAddr.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            catch (Exception e)
            {

                BaseUtils.log.Error(e);
            }
            return "";
        }

        /// <summary>
        /// Metodo para obtener la IPADDR externa del cliente
        /// </summary>
        /// <returns>Direccion IP externa</returns>
        public string getExternalIP()
        {
            string externalIp = "";
            try
            {
                externalIp = new WebClient().DownloadString(Properties.Settings.Default.dnsExternalConsult);
            }
            catch (Exception e)
            {

                BaseUtils.log.Error(e);
            }
            return externalIp;
        }
    }
}
