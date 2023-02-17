using Inventario.CQRS.Commands;
using Inventario.DTOs;
using Inventario.Models;
using Inventario.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Inventario.CQRS.Handlers
{
    public class InsertItemHandler : IRequestHandler<InsertItemCommand, Item>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task<Item> Handle(InsertItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Item
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                LastUpdated = request.LastUpdated,
                IdCategory = request.IdCategory,
                Category = request.Category,
                Brand = request.Brand,
                IdModel = request.IdModel,
                Model = request.Model,
                SerialNumber = request.SerialNumber,
                Location = request.Location,
                Status = request.Status,
                Notes = request.Notes,
                AddDate = request.AddDate,
                Stock = request.Stock,
                Price = request.Price
            };
            _unitOfWork.ItemRepository.Add(item);
            _unitOfWork.Save();
            return Task.FromResult(item);
        }
        /*public async Task<ItemDTO> Handle(InsertItemCommand request, CancellationToken cancellationToken)
        {
            return await request.SaveItemAsync();
        }*/
        
    }
}