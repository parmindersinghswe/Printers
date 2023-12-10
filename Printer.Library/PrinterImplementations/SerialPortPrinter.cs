using System.IO.Ports;
using Printer.Library.Utilities;
using Printer.Library.Models;
using Printer.Library.BaseClasses;
using Printer.Library.Exceptions;

namespace Printer.Library.PrinterImplementations;

public class SerialPortPrinter : PrinterBase<SerialPortPrinterModel>
{
    private SerialPort _serialPort { get; set; }
    private readonly SerialPortPrinterModel modal = new SerialPortPrinterModel();
    public SerialPortPrinter(string portName, int baudRate, bool dtrEnabled = true, int receivedBytesThreshold = 1)
    {
        modal.Port = portName;
        modal.BaudRate = baudRate;
        modal.DtrEnabled = dtrEnabled;
        modal.ReceivedBytesThreshold = receivedBytesThreshold;
        Configure(modal);
    }
    public override void Configure(SerialPortPrinterModel config)
    {

        _serialPort = new SerialPort(config.Port);
        _serialPort.BaudRate = config.BaudRate;
        _serialPort.DtrEnable = config.DtrEnabled;
        _serialPort.ReceivedBytesThreshold = config.ReceivedBytesThreshold;
    }
    public override void Print(string content)
    {
        try
        {
            _serialPort.Open();
            _serialPort.WriteLine(content);
        }
        catch (Exception e)
        {
            PrinterUtility.LogError(e.Message);
            throw new PrinterException(e.Message);
        }
        finally
        {
            _serialPort.Close();
        }
    }
    public override void Dispose()
    {
        if (_serialPort != null)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close(); // Close the SerialPort if it's open
            }
            _serialPort.Dispose(); // Dispose of the SerialPort
        }
    }
}
