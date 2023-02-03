using Inventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.ViewModels
{
    public class ItemFormViewModel
    {
        public Item Item { get; set; }
        public List<ItemModel> ItemModels { get; set; }
        public List<ItemCategory> ItemCategories { get; set; }
    }
}