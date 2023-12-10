using Printer.Library.Interfaces;

namespace Printer.MAUI.Blazor.Test.Utility;

public class PrintableContent : IPrintable
    {
        private readonly string content;

        public PrintableContent(string content)
        {
            this.content = content;
        }

        public string GetContent()
        {
            return content;
        }
    }
