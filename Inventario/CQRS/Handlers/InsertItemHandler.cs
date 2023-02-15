using Inventario.CQRS.Commands;
using Inventario.DTOs;
using Inventario.Models;
using Inventario.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Inventario.CQRS.Handlers
{
    public class InsertItemHandler : IRequestHandler<InsertItemCommand, ItemDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /*public async Task<ItemDTO> Handle(InsertItemCommand request, CancellationToken cancellationToken)
        {
            return await request.SaveItemAsync();
        }*/
        public Task<ItemDTO> Handle(InsertItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Item
            {
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                LastUpdated = request.LastUpdated,
                IdCategory = request.IdCategory,
                Brand = request.Brand,
                IdModel = request.IdModel,
                SerialNumber = request.SerialNumber,
                Location = request.Location,
                Status = request.Status,
                Notes = request.Notes,
                AddDate = request.AddDate,
                Stock = request.Stock,
                Price = (double)request.Price
            };

            _unitOfWork.ItemRepository.Add(item);
            _unitOfWork.Save();

            var itemDTO = ItemMapper.ToDTO(item);

            return Task.FromResult(itemDTO);
        }
    }
}