using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataImporter.Data;
using DataImporter.Membership.Entities;


namespace DataImporter.Importing.Entities
{
    public class Group : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<ExcelData> ExcelDatas { get; set; }
        public  List<ExcelFile> ExcelFiles { get; set; }
    }
}
