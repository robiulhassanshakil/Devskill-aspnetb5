using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public class Students :IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        
    }
}
