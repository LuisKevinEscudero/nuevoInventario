using Inventario.CQRS.Commands;
using Inventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario.DTOs
{
    public static class ItemMapper
    {
        public static ItemDTO ToDTO(Item item)
        {
            var model = new ItemModelDTO
            {
                Id = item.Model.Id,
                Name = item.Model.Name,
            };

            var category = new ItemCategoryDTO
            {
                Id = item.Category.Id,
                Name = item.Category.Name,
            };

            return new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                IdCategory = item.IdCategory,
                Category = category,
                Brand = item.Brand,
                IdModel = item.IdModel,
                Model = model,
                SerialNumber = item.SerialNumber,
                Location = item.Location,
                Status = item.Status,
                Notes = item.Notes,
                Stock = item.Stock,
                Price = item.Price,
                LastUpdated = item.LastUpdated,
                AddDate = item.AddDate
            };
        }



        public static Item ToItem(ItemDTO itemDTO) 
        {
            return new Item
            {
                Id = itemDTO.Id,
                Name = itemDTO.Name,
                Description = itemDTO.Description,
                Quantity = itemDTO.Quantity,
                IdCategory = itemDTO.IdCategory,
                Brand = itemDTO.Brand,
                IdModel = itemDTO.IdModel,
                SerialNumber = itemDTO.SerialNumber,
                Location = itemDTO.Location,
                Status = itemDTO.Status,
                Notes = itemDTO.Notes,
                Stock = itemDTO.Stock,
                Price = itemDTO.Price,
                LastUpdated = itemDTO.LastUpdated,
                AddDate = itemDTO.AddDate
            };
        } 
    }
}