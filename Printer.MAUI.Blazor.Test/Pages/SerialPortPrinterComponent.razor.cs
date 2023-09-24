using Microsoft.AspNetCore.Components;
using Printer.Library.Interfaces;
using Printer.Library.PrinterImplementations;
using Printer.MAUI.Blazor.Test.Utility;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer.MAUI.Blazor.Test.Pages
{
    public partial class SerialPortPrinterComponent : ComponentBase
    {
        private string _selectedComPort = "COM1";
        private int _selectedBaudRate = 9600;
        private bool printed = false;
        private string _printError = string.Empty;
        private string inputText = string.Empty;
        private int _printCount = 1;
        private bool _isBold { get; set; }
        private readonly string _boldStart = $"{(char)27}{(char)69}{(char)1}";
        private readonly string _boldEnd = $"{(char)27}{(char)69}{(char)0}";
        // Define available COM ports and baud rates here

        private List<string> _availableComPorts { get; set; }
        private int[] _availableBaudRates = { 9600, 19200, 38400, 57600, 115200 };

        protected override Task OnInitializedAsync()
        {
            _availableComPorts = new List<string>();
            for (int i = 1; i <= 32; i++)
            {
                _availableComPorts.Add($"COM{i}");
            }
            for (int i = 1; i <= 8; i++)
            {
                _availableComPorts.Add($"LPT{i}");
            }
            return base.OnInitializedAsync();
        }
        private async Task Print()
        {
            string printableText = _isBold ? _boldStart + inputText + _boldStart : inputText;
            PrintText(printableText);
            PrintText("\n\n\n\n");

        }
        private async Task PrintAndCut()
        {
            string printableText = _isBold ? _boldStart + inputText + _boldStart : inputText;
            PrintText(printableText);
            PrintText("\n\n\n\n");
            Cut();
        }
        private async Task Cut()
        {
            PrintText($"{(char)27}{(char)105}");
        }
        private async Task PrintText(string text)
        {
            _printError = string.Empty;
            try
            {
                using (var serialPrinter = new SerialPortPrinter(_selectedComPort, _selectedBaudRate))
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
            catch (Exception e)
            {
                _printError = e.Message;
                printed = false;
            }
        }
    }
}
