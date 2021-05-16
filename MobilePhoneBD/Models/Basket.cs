using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhoneBD.Models
{
    public class Basket
    {
        public int Id { get; set; }

        public int Quality { get; set; }
        [ForeignKey("Products")]
        public int ProductId { get; set; }

        public Products Products { get; set; }
    }
}
