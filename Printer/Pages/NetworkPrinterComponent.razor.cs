using Microsoft.AspNetCore.Components;
using Printer.Library.Interfaces;
using Printer.Library.PrinterImplementations;
using Printer.Utility;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Printer.Pages
{
    public partial class NetworkPrinterComponent : ComponentBase
    {
        private string _ipAddress = "127.0.0.1";
        private int _printerPort = 80;
        private bool printed = false;
        private string _printError = string.Empty;
        private string inputText = string.Empty;
        private int _printCount = 1;
        private bool _isBold { get; set; }
        private readonly string _boldStart = $"{(char)(27)}{(char)(69)}{(char)(1)}";
        private readonly string _boldEnd = $"{(char)(27)}{(char)(69)}{(char)(0)}";
        // Define available COM ports and baud rates here

        private async Task Print()
        {
            string printableText = _isBold ? _boldStart + inputText + _boldStart : inputText;
            PrintText(printableText);
            PrintText("\n\n\n\n");

        }
        private async Task PrintAndCut()
        {
            string printableText = _isBold ?  _boldStart + inputText + _boldStart : inputText;
            PrintText(printableText);
            PrintText("\n\n\n\n");
            Cut();
        }
        private async Task Cut()
        {
            PrintText($"{(char)(27)}{(char)(105)}");
        }
        private async Task PrintText(string text)
        {
            _printError = string.Empty;
            try
            {
                using (var serialPrinter = new NetworkPrinter(_ipAddress, _printerPort))
                {
                    // Step 2: Create an IPrintable instance with the text "Hello Parminder"
                    IPrintable printableContent = new PrintableContent(text);

                    // Step 3: Print the content using the SerialPortPrinter

                    if (_printCount <= 0)
                    {
                        _printCount = 1;
                    }
                    for (int i = 0; i < _printCount; i++)
                    {

                        serialPrinter.Print(printableContent);
                    }
                }
                printed = true;
            }
            catch(Exception e)
            {
                printed = false;
                _printError = e.Message;
            }
        }
    }
}
