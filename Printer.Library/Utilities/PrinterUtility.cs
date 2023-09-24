using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer.Library.Utilities
{
    internal static class PrinterUtility
    {
        internal static void LogError(string errorMessage)
        {
            // Implement logging of errors or exceptions, e.g., to a log file or console.
            Console.WriteLine($"Error: {errorMessage}");
        }

        internal static void ValidatePrinterSettings()
        {
            // Implement validation logic for printer settings, if needed.
            // Throw PrinterException if validation fails.
        }
    }
}
