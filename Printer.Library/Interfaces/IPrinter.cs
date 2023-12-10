namespace Printer.Library.Interfaces;

public interface IPrinter : IDisposable
{
    void Print(IPrintable printable);
}
