using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventario.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "The {0} can be at most {1} characters long.")]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} must be a positive number.")]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdated { get; set; }

        [Required]
        public int IdCategory { get; set; }
        public ItemCategory Category { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Brand { get; set; }


        [Required]
        public int IdModel { get; set; }
        public ItemModel Model { get; set; }

        [StringLength(100, ErrorMessage = "The {0} can be at most {1} characters long.")]
        public string SerialNumber { get; set; }

        [StringLength(100, ErrorMessage = "The {0} can be at most {1} characters long.")]
        public string Location { get; set; }

        [StringLength(100, ErrorMessage = "The {0} can be at most {1} characters long.")]
        public string Status { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} can be at most {1} characters long.")]
        public string Notes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddDate { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} must be a positive number.")]
        public int Stock { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "The {0} must be a positive number.")]
        public double Price { get; set; }
    }
}