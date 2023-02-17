using Inventario.DTOs;
using Inventario.Models;
using Inventario.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Inventario.CQRS.Commands
{
    public class InsertItemCommand : IRequest<Item>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public int IdCategory { get; set; }
        public ItemCategory Category { get; set; }
        public string Brand { get; set; }
        public int IdModel { get; set; }
        public ItemModel Model { get; set; }
        public string SerialNumber { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime AddDate { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }

        
        public InsertItemCommand(
            int id, string name, string description, int quantity, DateTime lastUpdated, 
            int idCategory,ItemCategory category, string brand, int idModel,ItemModel model , string serialNumber, string location, 
            string status, string notes, DateTime addDate, int stock, double price
        )
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            LastUpdated = lastUpdated;
            IdCategory = idCategory;
            Category = category;
            Brand = brand;
            IdModel = idModel;
            Model = model;
            SerialNumber = serialNumber;
            Location = location;
            Status = status;
            Notes = notes;
            AddDate = addDate;
            Stock = stock;
            Price = price;
        }
    }

}