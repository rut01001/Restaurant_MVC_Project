using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace uniqe_web.Models
{
    public class ModelData
    {
        public DataTable ListCategories { get; set; }
        public string IsAdmin { get; set; }
        public string NameDish { get; set; }
    }
}
