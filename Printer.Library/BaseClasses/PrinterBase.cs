using Printer.Library.Interfaces;

namespace Printer.Library.BaseClasses;

public abstract class PrinterBase<ConfigType> : IPrinterBase<ConfigType>
{
    public void Print(IPrintable printable)
    {
        Print(printable.GetContent());
    }
    public abstract void Print(string content);
    public abstract void Configure(ConfigType config); // Additional configuration options for printers.

    public abstract void Dispose();
}
