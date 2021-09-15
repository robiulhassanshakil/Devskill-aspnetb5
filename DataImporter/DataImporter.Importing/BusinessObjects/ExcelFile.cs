using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importing.BusinessObjects
{
    public class ExcelFile
    {
        public DateTime DateTime { get; set; }
        public string ExcelFileName { get; set; }
        public string ExcelFilePath { get; set;}
        public int GroupId { get; set; }
    }
}
