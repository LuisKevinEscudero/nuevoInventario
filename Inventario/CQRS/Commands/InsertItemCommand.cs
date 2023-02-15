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
    public class InsertItemCommand : IRequest<ItemDTO>
    {
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
        public decimal Price { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public InsertItemCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ItemDTO> SaveItemAsync()
        {
            var item = new Item
            {
                Name = Name,
                Description = Description,
                Quantity = Quantity,
                LastUpdated = LastUpdated,
                IdCategory = IdCategory,
                Brand = Brand,
                IdModel = IdModel,
                SerialNumber = SerialNumber,
                Location = Location,
                Status = Status,
                Notes = Notes,
                AddDate = AddDate,
                Stock = Stock,
                Price = (double)Price
            };

            _unitOfWork.ItemRepository.Add(item);
            await _unitOfWork.SaveChangesAsync();

            var itemDTO = ItemMapper.ToDTO(item);

            return itemDTO;
        }
    }

}