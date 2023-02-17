using Inventario.DTOs;
using Inventario.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Inventario.CQRS.Commands
{
    public class UpdateItemCommand : IRequest<ItemDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public int IdCategory { get; set; }
        public string Brand { get; set; }
        public int IdModel { get; set; }
        public string SerialNumber { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime AddDate { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public UpdateItemCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ItemDTO> UpdateItemAsync()
        {
            var item = _unitOfWork.ItemRepository.Get(Id);
            if (item == null)
            {
                throw new Exception($"Item with ID {Id} not found.");
            }

            item.Name = Name;
            item.Description = Description;
            item.Quantity = Quantity;
            item.LastUpdated = LastUpdated;
            item.IdCategory = IdCategory;
            item.Brand = Brand;
            item.IdModel = IdModel;
            item.SerialNumber = SerialNumber;
            item.Location = Location;
            item.Status = Status;
            item.Notes = Notes;
            item.AddDate = AddDate;
            item.Stock = Stock;
            item.Price = (double)Price;

            await _unitOfWork.SaveChangesAsync();

            var itemDTO = ItemMapper.ToDTO(item);

            return itemDTO;
        }
    }

}