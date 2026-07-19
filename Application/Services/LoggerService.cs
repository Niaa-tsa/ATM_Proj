using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Linq;
namespace Application.Services
{
    public class LoggerService : ILoggerService
    {
        string path = @"C:\Users\Nia Tsalkalamanidze\Desktop\N\Step\ATM_Project\Infrastructure\Data\Logs.txt";

        public void Log(string message)
        {
            string ip = GetUserIp();
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"[{DateTime.Now} - IP {ip}]" + message);
            }
        }

       private string GetUserIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }
    }
}
