using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhoneBD.Models
{
    public class Manufacturers
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Products> Products { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}
