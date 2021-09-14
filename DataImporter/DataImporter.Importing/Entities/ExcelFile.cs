using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Data;

namespace DataImporter.Importing.Entities
{
    public class ExcelFile : IEntity<int>
    {
        public int Id { get; set; }
        public string ExcelFileName { get; set; }
        public string ExcelFilePath { get; set; }
    }
}
