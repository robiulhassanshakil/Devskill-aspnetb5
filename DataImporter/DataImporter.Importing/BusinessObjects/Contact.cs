using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importing.BusinessObjects
{
    public class Contact
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
