namespace Printer.Library.Models
{
    public class SerialPortPrinterModel
    {
        public string Port { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public bool DtrEnabled { get; set; } = true;
        public int ReceivedBytesThreshold { get; set; } = 1;
    }
}
