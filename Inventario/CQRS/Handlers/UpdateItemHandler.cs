using Inventario.CQRS.Commands;
using Inventario.DTOs;
using Inventario.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using Inventario.Models;
using System.IO;

namespace Inventario.CQRS.Handlers
{
    public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, Item>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Item> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _unitOfWork.ItemRepository.Get(request.Id);

            if (item == null)
            {
                throw new Exception("Item not found");
            }
            
            item.Name = request.Name ?? item.Name;
            item.Description = request.Description ?? item.Description;
            item.Quantity = request.Quantity > 0 ? request.Quantity : item.Quantity;
            item.LastUpdated = request.LastUpdated != default ? request.LastUpdated : item.LastUpdated;
            item.IdCategory = request.IdCategory > 0 ? request.IdCategory : item.IdCategory;
            item.Category = request.Category ?? item.Category;
            item.Brand = request.Brand ?? item.Brand;
            item.IdModel = request.IdModel > 0 ? request.IdModel : item.IdModel;
            item.Model = request.Model ?? item.Model;
            item.SerialNumber = request.SerialNumber ?? item.SerialNumber;
            item.Location = request.Location ?? item.Location;
            item.Status = request.Status ?? item.Status;
            item.Notes = request.Notes ?? item.Notes;
            item.AddDate = request.AddDate != default ? request.AddDate : item.AddDate;
            item.Stock = request.Stock > 0 ? request.Stock : item.Stock;
            item.Price = request.Price > 0 ? request.Price : item.Price;         

            _unitOfWork.SaveAsync();

            return Task.FromResult(item);
        }
    }
}