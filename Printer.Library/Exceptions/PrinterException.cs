namespace Printer.Library.Exceptions;

internal class PrinterException : Exception
{
    internal PrinterException() { }

    internal PrinterException(string message) : base(message) { }

    internal PrinterException(string message, Exception innerException) : base(message, innerException) { }
}
