using System;
using System.ComponentModel.DataAnnotations;

namespace CommonwWeb.Models
{
    public class HomeViewModel
    {
        public string DataBase { get; set; }

        public string Iid { get; set; }

        public string FileType { get; set; }

    }

    public class ImageSearchModel
    {
        [Required]
        [StringLength(100)]
        public string Iid { get; set; }
    }
}
