using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string  Name { get; set; }
        [Required]
        [DefaultValueAttribute(false)]
        public Boolean Status { get; set; }
        
    }
}