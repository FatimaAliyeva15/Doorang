using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Doorang_Core.Models
{
    public class Card: BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string SubTitle { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        public string? ImgUrl { get; set; } = null!;
        [NotMapped]
        public IFormFile? ImgFile { get; set; } = null!;
    }
}
