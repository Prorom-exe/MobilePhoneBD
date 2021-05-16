using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhoneBD.Models
{
    public class Products
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quality { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Link { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [ForeignKey("Мanufacturers")]
        public int ManufacturerId { get; set; }

        public virtual Manufacturers Мanufacturers { get; set; }

        public ICollection<Basket> Baskets { get; set; }
    }
}
