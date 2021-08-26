using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}
