
using Printer.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer.Utility
{
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
}
