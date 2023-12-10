namespace Printer.Library.Interfaces;

internal interface INetworkPrinter : IPrinter
{
    void ResetNetworkStream();
}
