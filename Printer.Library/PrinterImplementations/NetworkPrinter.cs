using System.Text;
using System.Net.Sockets;
using Printer.Library.Utilities;
using Printer.Library.Interfaces;
using Printer.Library.BaseClasses;
using Printer.Library.Models;
using System.Net;
using Printer.Library.Exceptions;

namespace Printer.Library.PrinterImplementations
{
    public class NetworkPrinter : PrinterBase<NetworkPrinterModel>, INetworkPrinter
    {
        private TcpClient? _tcpClient = null;

        public NetworkPrinter(string ipAddress, int portNumber = 80)
        {
            var config = new NetworkPrinterModel();
            config.IpAddress = ipAddress;
            config.Port = portNumber;
            Configure(config);
        }

        public override void Configure(NetworkPrinterModel config)
        {
            _tcpClient = new TcpClient(config.IpAddress, config.Port);
        }
        public override void Print(string content)
        {
            try
            {
                ResetNetworkStream(); // Reset the network stream before each print
                if (_tcpClient.Connected)
                {
                    using (NetworkStream stream = _tcpClient.GetStream())
                    {
                        byte[] printData = Encoding.ASCII.GetBytes(content);
                        stream.Write(printData, 0, printData.Length);
                    }
                }
            }
            catch (Exception e)
            {
                PrinterUtility.LogError(e.Message);
                throw new PrinterException(e.Message);
            }
        }

        public void ResetNetworkStream()
        {
            if (_tcpClient.Connected)
            {
                _tcpClient.GetStream().Dispose();
            }
        }
        public override void Dispose()
        {
            if (_tcpClient != null)
            {
                if (_tcpClient.Connected)
                {
                    _tcpClient.GetStream().Dispose(); // Close the network stream if it's open
                }
                _tcpClient.Close(); // Close the TcpClient
                _tcpClient.Dispose(); // Dispose of the TcpClient
            }
        }

    }
}
