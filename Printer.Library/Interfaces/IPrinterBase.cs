namespace Printer.Library.Interfaces;

public interface IPrinterBase<ConfigType> : IPrinter
{
    void Configure(ConfigType config);
    void Print(string content);
}