using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int IdCategory { get; set; }
        public ItemCategoryDTO Category { get; set; }
        public string Brand { get; set; }
        public int IdModel { get; set; }
        public ItemModelDTO Model { get; set; }
        public string SerialNumber { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime AddDate { get; set; }
    }
}