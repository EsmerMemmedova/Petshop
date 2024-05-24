using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Slider:BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(30)]
         public string Designation {  get; set; }   
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? ImgFile { get; set; }
    }
}
