using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhoneBD.Models.ViewMode
{
    public class CreateViewModel
    {
        public List<Category> Category { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }

        public int CatId { get; set; }
        public string NewCat { get; set; }

        public int ManId { get; set; }
        public string NewMan { get; set; }

        public int Price { get; set; }
        public int Quality { get; set; }

        public IFormFile UploadedFile { get; set; }
    }
}
