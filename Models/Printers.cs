using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyPrinters.Models
{
    public class Printers
    {
        public int EngenPrintersID { get; set; }
        public string PrinterName { get; set; }
        public int PrinterMakeID { get; set; }

        public string PrinterMake { get; set; }
        public string FolderToMonitor { get; set; }
        public string OutputType { get; set; }
        public string FileOutput { get; set; }
        public bool Active { get; set; }
        public string CreateTimestamp { get; set; }

    }
}
